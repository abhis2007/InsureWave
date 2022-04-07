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
        public void AddBroker(Broker _brker);
        public List<Gender> AllGender();
        public User GetUserById(string UserId);
        public User GetUserByMail(string UserId);
        public List<Asset> GetAssets();
        public string BrokerIdByUserId(string _uid);
        public List<BuyerAssetVessel> AllVeselAssetsByuserId(string _s);
        public List<CountryCurrExchange> GetCountries();
        public List<Request> GetReqests();
        public List<BuyerAssetVessel> AllVesselAssets();
        public BuyerAssetVessel getAssetOfVesselById(int id);

        public BuyerAssetVessel getAssetOfVesselByUserIdAssetId(string uid,int aid);
        public void AddAssetInBrokerBuyer(BrokerBuyer asset);
        public void AddAssetInBuyerVessel(BuyerAssetVessel _asset);

        public List<Request> AllRequest();

        public void DeleteAssetVesselBId(int id,string uid);

        public List<BrokerBuyer> AllAssetOfBrokerBuyer();

        public void UpdateAssetOfBuyerByAId(int Aid,BuyerAssetVessel _asset);


    }
}
