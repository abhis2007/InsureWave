using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace RepoLayer
{
    public interface IUser
    {
        public void AddUser(User _user);
        public bool IsValid(string _uid,string _passwod);
        public List<Role> AllRole();
        public List<User> AllUser();
        public List<Broker> AllBroker();
        public void AddBroker(Broker _broker);
        public void AddInsurer(Insurer _insurer);
        public List<Gender> AllGender();
        public User GetUserById(string UserId);
        public CountryCurrExchange GetCountryByCoutryId(int id);
        public User GetUserByMail(string UserId);
        public List<Asset> GetAssets();
        public Asset GetAssetById(int id);
        public string BrokerIdByUserId(string _uid);
        public List<BuyerAssetVessel> AllVeselAssetsByuserId(string _s);
        public List<CountryCurrExchange> GetCountries();
        public List<Request> GetReqests();
        public List<BuyerAssetVessel> AllVesselAssets();
        public BuyerAssetVessel getAssetOfVesselById(int id);

        public BuyerAssetVessel getAssetOfVesselByUserIdAssetId(string uid,int aid);
        public void AddAssetInBrokerBuyer(BrokerBuyer asset);
        public void AddAssetInBuyerVessel(BuyerAssetVessel _asset);
        public void AddInFeedback(Feedback f);
        public List<Feedback> GetFeedbackByUserIdAssetId(string uid,int Aid);

        public List<Request> AllRequest();

        public void DeleteAssetVesselBId(int id,string uid);

        public List<BrokerBuyer> AllAssetOfBrokerBuyer();

        public List<InsurerBroker> AllAssetOfInsurerBroker();

        public void UpdateAssetOfBuyerByAIdUId(int Aid,string uid,BuyerAssetVessel _asset);
        public BrokerBuyer GetAssetFromBrokerBuyerById(int AssetId);

        public void AddIntoInsurerBroker(InsurerBroker _Instance);

        public InsurerBroker GetAssetFromInsuerBrokerByBuyerIdAssetId(string BuyerId,int AssetId);

        public void AddInPolicy(PolicyDetail policy);

        public void AddInPremiumAmountDetails(PremiumAmountDetail _data);

        public PolicyDetail GetAssetIdFromPolicyDetailByBuyerIdAssetId(string buyerid,int assetid);

        public BrokerBuyer GetAssetFromBrokerBuyerByBuyerIdAssetId(string bid,int aid);

        public List<User> GetAllUserFromRoleId(int RoleId);

        public Role GetRoleByRoleType(string role);

        public void DeleteFeedbackByUserIdAssetId(string Uid, int Aid);
    }
}
