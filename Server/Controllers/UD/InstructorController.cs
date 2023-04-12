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
    public class InstructorController : BaseController
    {
        public InstructorController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("Instructor")]
        public async Task<IActionResult> GetInstructor()
        {
            List<InstructorDTO> lst = await _context.Instructors
                .Select(sp => new InstructorDTO
                {
                    ModifiedBy = sp.ModifiedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    InstructorId = sp.InstructorId,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetInstructor/{_SchoolID}")]
        public async Task<IActionResult> GetInstructor(int _SchoolID)
        {
            InstructorDTO? lst = await _context.Instructors
                .Where(x => x.SchoolId == _SchoolID)
                .Select(sp => new InstructorDTO
                {
                    ModifiedBy = sp.ModifiedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    InstructorId = sp.InstructorId,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }
    }
}
