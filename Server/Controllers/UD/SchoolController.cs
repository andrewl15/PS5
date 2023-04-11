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
    public class SchoolController : BaseController
    {
        public SchoolController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetSchool")]
        public async Task<IActionResult> GetSchool()
        {
            List<SchoolDTO> lst = await _context.Schools
                .Select(sp => new SchoolDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    SchoolId = sp.SchoolId,
                    SchoolName = sp.SchoolName
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetSchool/{_SchoolID")]
        public async Task<IActionResult> GetSchool(int _SchoolID)
        {
            SchoolDTO? lst = await _context.Schools
                .Where(x => x.SchoolId == _SchoolID)
                .Select(sp => new SchoolDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    SchoolId = sp.SchoolId,
                    SchoolName = sp.SchoolName
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }
    }
}
