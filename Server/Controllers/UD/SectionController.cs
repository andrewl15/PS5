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
    public class SectionController : BaseController
    {
        public SectionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            List<SectionDTO> lst = await _context.Sections
                .Select(sp => new SectionDTO
                {
                   Capacity = sp.Capacity,
                   CourseNo = sp.CourseNo,
                   CreatedBy = sp.CreatedBy,
                   CreatedDate = sp.CreatedDate,    
                   InstructorId = sp.InstructorId,
                   Location = sp.Location,
                   ModifiedBy = sp.ModifiedBy,
                   ModifiedDate = sp.ModifiedDate,
                   SchoolId = sp.SchoolId,
                   SectionId = sp.SectionId,
                   SectionNo = sp.SectionNo,
                   StartDateTime = sp.StartDateTime
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetSection/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> GetSection(int _SectionId, int _SchoolId)
        {
            SectionDTO? lst = await _context.Sections
                .Where(x => x.SectionId == _SectionId && x.SchoolId == _SchoolId)
                .Select(sp => new SectionDTO
                {
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostSection")]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section? c = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId && x.SchoolId == _SectionDTO.SchoolId).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Section
                    {
                        CourseNo = _SectionDTO.CourseNo,
                        SectionNo = _SectionDTO.SectionNo,
                        StartDateTime = _SectionDTO.StartDateTime,
                        Location = _SectionDTO.Location,
                        InstructorId= _SectionDTO.InstructorId,
                        Capacity = _SectionDTO.Capacity,

                    };
                    _context.Sections.Add(c);
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
        [Route("PutSection")]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section? c = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId && x.SchoolId == _SectionDTO.SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.CourseNo = _SectionDTO.CourseNo;
                    c.SectionNo = _SectionDTO.SectionNo;
                    c.StartDateTime = _SectionDTO.StartDateTime;
                    c.Location = _SectionDTO.Location;
                    c.InstructorId = _SectionDTO.InstructorId;
                    c.Capacity = _SectionDTO.Capacity;

                    _context.Sections.Update(c);
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
        [Route("DeleteSection/{_SectionId}/{_SchoolId}")]
        public async Task<IActionResult> DeleteSection(int _SectionId, int _SchoolId)
        {
            try
            {
                Section? c = await _context.Sections.Where(x => x.SectionId == _SectionId && x.SchoolId == _SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Sections.Remove(c);
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
