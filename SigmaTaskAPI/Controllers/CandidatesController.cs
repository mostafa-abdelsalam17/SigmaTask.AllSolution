using AutoMapper;
using BusinessLogic.Layer.Interfaces;
using DataAccess.Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        public CandidatesController(ICandidateRepository candidateRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            this._candidateRepository = candidateRepository;
            this._mapper = mapper;
            this._cache = memoryCache;
        }


        [HttpPost("SaveCandidate")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> SaveCandidateAsync([FromBody] CandidateDto candidateDto)
        {

            #region Try to apply cashe
            //// Try to get candidate from cache
            ////if (_cache.TryGetValue(candidateDto.Email, out Candidate? existingCandidate))
            ////{
            //    // Update candidate in cache
            //    existingCandidate?.Update(candidateDto.FirstName, candidateDto.LastName, candidateDto.PhoneNumber,
            //                             candidateDto.Email, candidateDto.BestCallTime, candidateDto.LinkedInProfileUrl,
            //                             candidateDto.GitHubProfileUrl, candidateDto.Comment);

            //    // Update candidate in database without explicitly reloading it from the database
            //    //await _candidateRepository.ExecuteUpdateAsync(c => c.Email == candidateDto.Email, candidateDto);
            //    existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            //    await _candidateRepository.UpdateAsync(existingCandidate);
            //}
            //else
            //{
            //    existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            //    if (existingCandidate == null)
            //    {
            //        var newCandidate = _mapper.Map<CandidateDto, Candidate>(candidateDto);
            //        await _candidateRepository.AddAsync(newCandidate);
            //    }
            //}

            ////_cache.Set(candidateDto.Email, existingCandidate);
            //return Ok(new ApiResponse(StatusCodes.Status200OK));
            #endregion

            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            if (existingCandidate == null)
            {
                var newCandidate = _mapper.Map<CandidateDto, Candidate>(candidateDto);
                await _candidateRepository.AddAsync(newCandidate);
            }
            else
            {
                existingCandidate?.Update(candidateDto.FirstName, candidateDto.LastName, candidateDto.PhoneNumber,
                                       candidateDto.Email, candidateDto.BestCallTime, candidateDto.LinkedInProfileUrl,
                                         candidateDto.GitHubProfileUrl, candidateDto.Comment);

                await _candidateRepository.UpdateAsync(existingCandidate);
            }
             return Ok(new ApiResponse(StatusCodes.Status200OK));
            
        }
    }
}
