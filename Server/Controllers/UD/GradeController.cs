﻿using DOOR.EF.Data;
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
    public class GradeController : BaseController
    {
        public GradeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade()
        {
            List<GradeDTO> lst = await _context.Grades
                .Select(sp => new GradeDTO
                {
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    SchoolId = sp.SchoolId,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    GradeTypeCode = sp.GradeTypeCode,
                    NumericGrade = sp.NumericGrade,
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGrade/{_SchoolID}/{_GradeCodeOccurrence}/{_GradeTypeCode}/{_SectionID}/{_StudentID}")]
        public async Task<IActionResult> GetGrade(int _SchoolID, byte _GradeCodeOccurrence, int _StudentID, int _SectionID, string _GradeTypeCode)
        {
            GradeDTO? lst = await _context.Grades
                .Where(x => x.SchoolId == _SchoolID && x.GradeCodeOccurrence == _GradeCodeOccurrence && x.StudentId == _StudentID && x.SectionId == _SectionID && x.GradeTypeCode == _GradeTypeCode)
                .Select(sp => new GradeDTO
                {
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    SchoolId = sp.SchoolId,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    GradeTypeCode = sp.GradeTypeCode,
                    NumericGrade = sp.NumericGrade,
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostGrade")]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade? c = await _context.Grades.Where(x => x.SchoolId == _GradeDTO.StudentId && x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence && x.StudentId == _GradeDTO.StudentId && x.SectionId == _GradeDTO.SectionId && x.GradeTypeCode == _GradeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Grade
                    {
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments
                    };
                    _context.Grades.Add(c);
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
        [Route("PutGrade")]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade? c = await _context.Grades.Where(x => x.SchoolId == _GradeDTO.StudentId && x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence && x.StudentId == _GradeDTO.StudentId && x.SectionId == _GradeDTO.SectionId && x.GradeTypeCode == _GradeDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.NumericGrade = _GradeDTO.NumericGrade;
                    c.Comments = _GradeDTO.Comments;

                    _context.Grades.Update(c);
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
        [Route("DeleteGrade/{_SchoolID}/{_GradeCodeOccurrence}/{_GradeTypeCode}/{_SectionID}/{_StudentID}")]
        public async Task<IActionResult> DeleteGrade(int _SchoolID, byte _GradeCodeOccurrence, int _StudentID, int _SectionID, string _GradeTypeCode)
        {
            try
            {
                Grade? c = await _context.Grades.Where(x => x.SchoolId == _SchoolID && x.GradeCodeOccurrence == _GradeCodeOccurrence && x.StudentId == _StudentID && x.SectionId == _SectionID && x.GradeTypeCode == _GradeTypeCode).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Grades.Remove(c);
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
