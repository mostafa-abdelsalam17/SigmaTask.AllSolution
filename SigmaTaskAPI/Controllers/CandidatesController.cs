using AutoMapper;
using BusinessLogic.Layer.Interfaces;
using DataAccess.Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaTaskAPI.DTOs;
using SigmaTaskAPI.Errors;

namespace SigmaTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        public CandidatesController(ICandidateRepository candidateRepository, IMapper mapper)
        {
            this._candidateRepository = candidateRepository;
            this._mapper = mapper;
        }


        [HttpPost("SaveCandidate")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> SaveCandidateAsync([FromBody] CandidateDto candidateDto)
        {
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            if (existingCandidate == null)
            {
                var newCandidate = _mapper.Map<CandidateDto, Candidate>(candidateDto);
                await _candidateRepository.AddAsync(newCandidate);
                return Ok(new ApiResponse(StatusCodes.Status200OK));
            }
            else
            {
                existingCandidate.Update(candidateDto.FirstName, candidateDto.LastName,candidateDto.PhoneNumber,candidateDto.Email
                    ,candidateDto.BestCallTime,candidateDto.LinkedInProfileUrl,candidateDto.GitHubProfileUrl, candidateDto.Comment);
               
                await _candidateRepository.UpdateAsync(existingCandidate);
                return Ok(new ApiResponse(StatusCodes.Status200OK));
            }
        }
    }
}
