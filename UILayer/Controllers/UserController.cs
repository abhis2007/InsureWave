using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RepoLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using UILayer.Models;
using Microsoft.AspNetCore.Http;

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
        public IActionResult CreateUser(NewUser _NewUser)
        {
            User _user = new User() { UserId=_NewUser.UserId,FirstName=_NewUser.FirstName,LastName=_NewUser.LastName,GenderId=_NewUser.GenderId,EmailId=_NewUser.EmailId,Password=_NewUser.Password};
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
            if (_user.RoleId == 1) //Broker
            {
                Broker broker = new Broker() { BrokerId = "BR_" + _user.UserId, UserId = _user.UserId };
                obj.AddBroker(broker);
            }
            if (_user.RoleId == 6)
            {
                Insurer insurer = new Insurer() {InsurerId="IR_"+_user.UserId,UserId=_user.UserId };
                obj.AddInsurer(insurer);
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
                HttpContext.Session.SetString("UserName",model.UserName);
                User _user= obj.GetUserById(model.UserName);
                if (_user.RoleId == model.RoleId && model.RoleId == 1)return RedirectToAction("Broker");
                if (_user.RoleId == model.RoleId && model.RoleId == 6) return RedirectToAction("Insurer");
                if (_user.RoleId == model.RoleId && model.RoleId == 11)return RedirectToAction("BuyerPageAfterLogin");
            }
            ViewBag.Msg = "Invalid attempt";
            ViewBag.RoleList = new SelectList(obj.AllRole(), "RoleId", "RoleName");
            return View();
        }

        public IActionResult BuyerPageAfterLogin()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            ViewBag.Assets = obj.GetAssets().ConvertAll(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.AssetId.ToString()
                };
            });
            ViewBag.Countries = obj.GetCountries().ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.CountryName,
                    Value = x.CountryId.ToString()
                };
            });
            ViewBag.Requests = obj.AllRequest().ConvertAll(x => {
                return new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(UserName);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }

        [HttpPost]
        public IActionResult BuyerPageAfterLogin(BuyerAssetVessel _val)
        {
            _val.UserId =HttpContext.Session.GetString("UserName");
            ViewBag.Assets = obj.GetAssets().ConvertAll(x=> {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.AssetId.ToString()
                };
            });
            ViewBag.Countries = obj.GetCountries().ConvertAll(x=>{
                return new SelectListItem {
                    Text=x.CountryName,
                    Value=x.CountryId.ToString()
                };
            });
            ViewBag.Requests = obj.AllRequest().ConvertAll(x=> {
                return new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });

            ViewBag.AllAssetOfVessel = obj.AllVeselAssetsByuserId(_val.UserId);
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllCountry = obj.GetCountries();

            BuyerAssetVessel ExistingAsset = obj.getAssetOfVesselByUserIdAssetId(_val.UserId,_val.AssetId);
            
            if (ExistingAsset == null)
            {
                obj.AddAssetInBuyerVessel(_val);
                return RedirectToAction("BuyerPageAfterLogin");
            }
            obj.UpdateAssetOfBuyerByAIdUId(_val.AssetId,_val.UserId, _val);
            return RedirectToAction("BuyerPageAfterLogin");
        }

        public IActionResult Broker()
        {
            string UserName=HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            ViewBag.AllAssetOfVessel = obj.AllVesselAssets();
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllBrokerBuyerAsset = obj.AllAssetOfBrokerBuyer();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }
        
        [HttpPost]
        public IActionResult Broker(BrokerBuyer model) {
            string brokerId = obj.BrokerIdByUserId(HttpContext.Session.GetString("UserName"));
            model.BrokerId = brokerId;
            model.PolicyStatus = 10;
            obj.AddAssetInBrokerBuyer(model);

            ViewBag.AllAssetOfVessel = obj.AllVesselAssets();
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllBrokerBuyerAsset = obj.AllAssetOfBrokerBuyer();
            ViewBag.AllCountry = obj.GetCountries();
            return View();
        }
        
        public IActionResult RemoveFromBuyerInsList(int AID,string UID)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            obj.DeleteAssetVesselBId(AID,UID);
            return RedirectToAction("BuyerPageAfterLogin");
        }
        
        public IActionResult Insurer()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null) return RedirectToAction("LoginPage");
            ViewBag.AllAssetOfBuyerBroker= obj.AllAssetOfBrokerBuyer();
            ViewBag.AllAsset = obj.GetAssets();
            ViewBag.AllUser = obj.AllUser();
            return View();
        }

        public IActionResult InsurerResponse(int AId,int Status){
            string UserName = HttpContext.Session.GetString("UserName");
            BrokerBuyer ExistingAsset= obj.GetAssetFromBrokerBuyerById(AId);
            InsurerBroker InsurerBroker = new InsurerBroker() { BrokerId=ExistingAsset.BrokerId,InsurerId="IR_"+UserName,AssetId=AId,BrokerageCharge=10000,BuyerId=ExistingAsset.UserId,InsuranceTenure=ExistingAsset.InsuranceTenure};
            obj.AddIntoInsurerBroker(InsurerBroker);
            InsurerBroker ExistingAsset2 = obj.GetAssetFromInsuerBrokerByBuyerIdAssetId(ExistingAsset.UserId,AId);
            string PolicyStatus;
            if (Status == 1) PolicyStatus = "Accepting your proposal for " + AId + " and will forward you all the details regarding insurance";
            else PolicyStatus = "Rejecting your proposal for asset: " + AId + " and will forward you all the details regarding rejection";
            PolicyDetail NewPolicy = new PolicyDetail(){Ibid=ExistingAsset2.Ibid,PolicyStatus=PolicyStatus,AssetId=AId,BuyerId=ExistingAsset.UserId};
            obj.AddInPolicy(NewPolicy);
            if (Status == 1)
            {
                PolicyDetail ExistingPolicy = obj.GetAssetIdFromPolicyDetailByBuyerIdAssetId(ExistingAsset.UserId, AId);
                DateTime today = DateTime.Now;
                string StartDate = today.ToLongDateString();
                string EndDate = today.AddYears(InsurerBroker.InsuranceTenure).ToLongDateString();
                int PremAmt = 150000;
                PremiumAmountDetail premdetails = new PremiumAmountDetail() { Pid = ExistingPolicy.Pid, IntervalOfEmi = 6, StartDate = StartDate, EndDate = EndDate, DownPay = (float)(0.2 * PremAmt), PremAmt = PremAmt, Tenure = InsurerBroker.InsuranceTenure, PreType = "Default"};
                obj.AddInPremiumAmountDetails(premdetails);
            }
            return RedirectToAction("Insurer");
        }
        
        public IActionResult Signout()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if(UserName!=null)HttpContext.Session.SetString("UserName","null");
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }

    }
}
