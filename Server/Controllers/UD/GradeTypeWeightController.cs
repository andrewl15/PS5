using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;

namespace DOOR.Server.Controllers.UD
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeTypeWeightController : BaseController
    {
        public GradeTypeWeightController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGradeTypeWeight")]
        public async Task<IActionResult> GetGradeTypeWeight()
        {
            List<GradeTypeWeightDTO> lst = await _context.GradeTypeWeights
                .Select(sp => new GradeTypeWeightDTO
                {
                    CreatedBy = sp.CreatedBy,
                    DropLowest = sp.DropLowest,
                    SchoolId = sp.SchoolId,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedDate = sp.CreatedDate,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    SectionId = sp.SectionId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeTypeWeight/{_SchoolId}")]
        public async Task<IActionResult> GetGradeTypeWeight(int _SchoolId)
        {
            GradeTypeWeightDTO? lst = await _context.GradeTypeWeights
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new GradeTypeWeightDTO
                {
                    CreatedBy = sp.CreatedBy,
                    DropLowest = sp.DropLowest,
                    SchoolId = sp.SchoolId,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedDate = sp.CreatedDate,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    SectionId = sp.SectionId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGradeTypeWeight")]
        public async Task<IActionResult> PostGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                GradeTypeWeight? c = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new GradeTypeWeight
                    {
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest
                    };
                    _context.GradeTypeWeights.Add(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }








        [HttpPut]
        [Route("PutGradeTypeWeight")]
        public async Task<IActionResult> PutGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                GradeTypeWeight? c = await _context.GradeTypeWeights.Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                    c.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                    c.DropLowest = _GradeTypeWeightDTO.DropLowest;

                    _context.GradeTypeWeights.Update(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }


        [HttpDelete]
        [Route("DeleteGradeTypeWeight/{_SchoolId}")]
        public async Task<IActionResult> DeleteGradeTypeWeight(int _SchoolId)
        {
            try
            {
                GradeTypeWeight? c = await _context.GradeTypeWeights.Where(x => x.SchoolId == _SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.GradeTypeWeights.Remove(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }
    }
}
