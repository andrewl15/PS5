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
    public class StudentController : BaseController
    {
        public StudentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("Student")]
        public async Task<IActionResult> GetStudent()
        {
            List<StudentDTO> lst = await _context.Students
                .Select(sp => new StudentDTO
                {
                    Zip = sp.Zip,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    CreatedDate = sp.CreatedDate,
                    CreatedBy = sp.CreatedBy,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetStudent/{_StudentID}")]
        public async Task<IActionResult> GetZipcode(int _StudentID)
        {
            StudentDTO? lst = await _context.Students
                .Where(x => x.StudentId == _StudentID)
                .Select(sp => new StudentDTO
                {
                    Zip = sp.Zip,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    CreatedDate = sp.CreatedDate,
                    CreatedBy = sp.CreatedBy,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }
    }
}
