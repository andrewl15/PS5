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
        [Route("GetGrade/{_SchoolID")]
        public async Task<IActionResult> GetGrade(int _SchoolID)
        {
            GradeDTO? lst = await _context.Grades
                .Where(x => x.SchoolId == _SchoolID)
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
    }
}
