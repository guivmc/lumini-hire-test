using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuminiHire.Models;
using Microsoft.AspNetCore.Authorization;
using LuminiHire.Authentication;

namespace LuminiHire.Controllers
{
    [Route( "UniData" )]
    [ApiController]
    public class UniDataController : Controller
    {
        private readonly UniContext _UniContext;

        private readonly IUserService _UserService;

        public UniDataController( UniContext uniContext, IUserService userService )
        {
            this._UniContext = uniContext;
            this._UserService = userService;
        }

        #region Login and SingUp
        [HttpGet( "Index" )]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost( "Login" )]
        public IActionResult Login( [FromForm] User user )
        {
            if( ModelState.IsValid )
            {
                var login = this._UserService.GetUserContext().User.SingleOrDefault( o => o.UserName.Equals( user.UserName ) );

                if( login != null )
                    if( login.Password.Equals( user.Password ) )
                    {
                        return RedirectToAction( "GetUniversitiesList", this._UniContext.Student.ToList() );
                    }

                ViewBag.Message = String.Format( "Username and/or Password are incorrect!!!" );

                return View( "~/Views/UniData/Index.cshtml" );
            }

            return RedirectToAction( "GetErrorPage" );
        }

        [HttpGet( "SingUp" )]
        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost( "SingUp" )]
        public IActionResult SingUp( [FromForm] User user )
        {
            if( ModelState.IsValid )
            {
                var isNameUnique = this._UserService.GetUserContext().User.SingleOrDefault( o => o.UserName.Equals( user.UserName ) );

                if( isNameUnique == null )
                {
                    this._UserService.GetUserContext().User.Add( user );
                    this._UserService.GetUserContext().SaveChanges();
                    return RedirectToAction( "Index" );
                }

                ViewBag.Message = String.Format( "Username already in use!" );

                return View();
            }

            return RedirectToAction( "GetErrorPage" );
        }
        #endregion

        [HttpGet( "GetErrorPage" )]
        public IActionResult GetErrorPage()
        {
            return View( "~/Views/UniData/Error.cshtml" );
        }

        [HttpGet( "GetUniversitiesList" )]
        public IActionResult GetUniversitiesList()
        {
            return View( this._UniContext.Student.ToList() );
        }

        [HttpGet( "JsonRedirectToAddEdit" )]
        public IActionResult JsonRedirectToAddEdit( long id )
        {
            return Json( new { success = true, redirectUrl = "/UniData/AddEditUniversity?id=" + id } );
        }

        [HttpGet( "AddEditUniversity" )]
        public IActionResult AddEditUniversity( long id )
        {
            var uni = this._UniContext.Student.SingleOrDefault( o => o.UNITID == id );

            if( uni != null )
                return View( "~/Views/UniData/AddEditUniversity.cshtml", uni );

            return View( "~/Views/UniData/AddEditUniversity.cshtml", new UniItem() );
        }

        [HttpPost( "AddEditUniversity" )]
        public IActionResult AddEditUniversity( [FromForm] UniItem item )
        {
            if( ModelState.IsValid )
            {
                if( item.UNITID == 0 )
                    this._UniContext.Add( item );
                else
                    this._UniContext.Update( item );

                this._UniContext.SaveChanges();

                return RedirectToAction( "GetUniversitiesList" );
            }

            return RedirectToAction( "GetErrorPage" );
        }

        [HttpGet( "DeleteUniversity" )]
        public IActionResult DeleteUniversity( long id )
        {
            var uni = this._UniContext.Student.SingleOrDefault( o => o.UNITID == id );

            if( uni != null )
            {
                this._UniContext.Student.Remove( uni );

                this._UniContext.SaveChanges();
            }

            return Json( new { success = true, redirectUrl = "/UniData/GetUniversitiesList" } );

        }
    }
}
