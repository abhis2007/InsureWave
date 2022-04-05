using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RepoLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using UILayer.Models;

namespace UILayer.Controllers
{
    public class UserController : Controller
    {
        IUser obj;
        public UserController(IUser _obj)
        {
            obj = _obj;
        }

        public IActionResult CreateUser()
        {
            var RolesList=obj.AllRole();
            var GenList = obj.AllGender();
            ViewBag.RoleList = new SelectList(RolesList,"RoleId", "RoleName");
            ViewBag.GenderList = new SelectList(GenList, "GenderId", "GenderName");

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User _user)
        {
            var RolesList = obj.AllRole();
            var GenList = obj.AllGender();
            ViewBag.RoleList = new SelectList(RolesList, "RoleId", "RoleName");
            ViewBag.GenderList = new SelectList(GenList, "GenderId", "GenderName");
            
            if (obj.GetUserById(_user.UserId) != null)
            {
                ViewBag.IsValidId =_user.UserId +" already exist";
                return View();
            }
            if (obj.GetUserByMail(_user.EmailId) != null)
            {
                ViewBag.IsValidMail = _user.EmailId + " already exist";
                return View();
            }
            obj.AddUser(_user);
            if (_user.RoleId == 1)
            {
                Broker broker = new Broker() { BrokerId = "BR_" + _user.UserId, UserId = _user.UserId };
                obj.AddBroker(broker);
            }
            return RedirectToAction("CreateUser");
        }

        public IActionResult LoginPage()
        {
            var RolesList = obj.AllRole();
            ViewBag.RoleList = new SelectList(RolesList, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(Login model)
        {
            if (obj.IsValid(model.UserName, model.Password))
            {
                TempData["UserId"] = model.UserName;
                User _user= obj.GetUserById(model.UserName);
                if (_user.RoleId == model.RoleId && model.RoleId == 1)return RedirectToAction("Broker");
                if (_user.RoleId == model.RoleId && model.RoleId == 11)return RedirectToAction("BuyerPageAfterLogin");
            }
            ViewBag.Msg = "Invalid attempt";
            ViewBag.RoleList = new SelectList(obj.AllRole(), "RoleId", "RoleName");
            return View();
        }

        public IActionResult BuyerPageAfterLogin()
        {
            var AllAssets=obj.GetAssets();
            ViewBag.Assets = new SelectList(AllAssets, "AssetId", "Name");

            var AllCountry = obj.GetCountries();
            ViewBag.Countries = new SelectList(AllCountry, "CountryId", "CountryName");

            var AllRequest= obj.GetReqests();
            ViewBag.Requests = new SelectList(AllRequest, "Id", "Name");

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(Convert.ToString(TempData["UserId"]));

            ViewBag.AllAsset = AllAssets;
            ViewBag.AllCountry = AllCountry;
            TempData["UserId"] = TempData["UserId"];
            return View();
        }

        [HttpPost]
        public IActionResult BuyerPageAfterLogin(BuyerAssetVessel _val)
        {
            _val.UserId =Convert.ToString(TempData["UserId"]);
            obj.AddAssetInBuyerVessel(_val);
            TempData["UserId"] = _val.UserId;

            ViewBag.Assets = new SelectList(obj.GetAssets(), "AssetId", "Name");
            ViewBag.Countries = new SelectList(obj.GetCountries(), "CountryId", "CountryName");
            ViewBag.Requests = new SelectList(obj.GetReqests(), "Id", "Name");

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(_val.UserId);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }
        

        public IActionResult Broker()
        {
            ViewBag.AllAssetOfVessel = obj.AllVesselAssets();
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }

        [HttpPost]
        public IActionResult Broker(BrokerBuyer model) {
            model.PolicyStatus = 10;
            model.BrokerId = obj.BrokerIdByUserId(model.UserId);
            obj.AddAssetInBrokerBuyer(model);

            ViewBag.AllAssetOfVessel = obj.AllVesselAssets();
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }
    }
}
