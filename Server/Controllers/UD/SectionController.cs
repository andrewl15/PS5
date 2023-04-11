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
        [Route("GetSection/{_SectionID")]
        public async Task<IActionResult> GetSection(int _SectionID)
        {
            SectionDTO? lst = await _context.Sections
                .Where(x=>x.SectionId == _SectionID)
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
    }
}
