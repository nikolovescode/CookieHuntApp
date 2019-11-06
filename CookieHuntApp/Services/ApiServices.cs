using CookieHuntApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookieHuntApp.Services
{
    class ApiServices
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            /* var calendarUser = new CalendarUser()
             {
                 Email = email,
                 Password = password
             };
             */
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://cookiehunterproject.azurewebsites.net/api/Account/Register", content);
            // await RegisterCalendarUser(calendarUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues = new List<KeyValuePair<string, string>>()
             {
               new KeyValuePair<string, string>("username", email),
               new KeyValuePair<string, string>("password", password),
               new KeyValuePair<string, string>("grant_type", "password"),
             };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://cookiehunterproject.azurewebsites.net/Token");
            request.Content = new FormUrlEncodedContent(keyvalues);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content);
            var accessToken = jObject.Value<string>("access_token");
            Settings.Accesstoken = accessToken;
            Settings.UserName = email;
            Settings.Password = password;
            return response.IsSuccessStatusCode;
        }

        public async Task<List<StandardCategory>> GetStandardCategories()
        {
            var httpClient = new HttpClient();
            var sCApiUrl = "http://cookiehunterproject.azurewebsites.net/api/StandardCategories";
            var json = await httpClient.GetStringAsync($"{sCApiUrl}");
            return JsonConvert.DeserializeObject<List<StandardCategory>>(json);
        }

        public async Task<bool> RegisterStore(string name)
        {
            var store = new Store()
            {
                Name = name
            };
            var json = JsonConvert.SerializeObject(store);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var sApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Stores";
            var response = await httpClient.PostAsync(sApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterStandardCategory(string title)
        {
            var standardCategory = new StandardCategory()
            {
                Title = title
            };
            var json = JsonConvert.SerializeObject(standardCategory);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var sCApiUrl = "http://cookiehunterproject.azurewebsites.net/api/StandardCategories";
            var response = await httpClient.PostAsync(sCApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterGroupACategory(int storeId, int standardCatId, int points, int percentOff)
        {
            var groupACategory = new GroupACategory()
            {
                StoreId = storeId,
                StandardCategoryId = standardCatId,
                Points = points,
                PercentOff = percentOff
            };
            var json = JsonConvert.SerializeObject(groupACategory);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var gACApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupACategories";
            var response = await httpClient.PostAsync(gACApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterGroupBCategory(int storeId, int standardCatId, int points, int percentOff)
        {
            var groupBCategory = new GroupBCategory()
            {
                StoreId = storeId,
                StandardCategoryId = standardCatId,
                Points = points,
                PercentOff = percentOff
            };
            var json = JsonConvert.SerializeObject(groupBCategory);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var gBCApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupBCategories";
            var response = await httpClient.PostAsync(gBCApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterGroupCCategory(int storeId, int standardCatId, int points, int percentOff)
        {
            var groupCCategory = new GroupCCateory()
            {
                StoreId = storeId,
                StandardCategoryId = standardCatId,
                Points = points,
                PercentOff = percentOff
            };
            var json = JsonConvert.SerializeObject(groupCCategory);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var gCCApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupCCategories";
            var response = await httpClient.PostAsync(gCCApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterStoreHasGroups(int storeId, int catAId, int catBId, int catCId)
        {

            var storeHasGroups = new StoreHasGroups()
            {
                StoreId = storeId,
                GroupACategoryId = catAId,
                GroupBCategoryId = catBId,
                GroupCCategoryId = catCId
            };
            var json = JsonConvert.SerializeObject(storeHasGroups);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var sHGApiUrl = "http://cookiehunterproject.azurewebsites.net/api/StoreHasGroups";
            var response = await httpClient.PostAsync(sHGApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Store>> GetStoreFromName(string name)
        {
            var httpClient = new HttpClient();
            var workTaskApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Stores";
            var json = await httpClient.GetStringAsync($"{workTaskApiUrl}?name={name}");
            return JsonConvert.DeserializeObject<List<Store>>(json);
        }

        public async Task<List<GroupACategory>> GetGroupAFromStoreId(int storeId)
        {
            var httpClient = new HttpClient();
            var gAApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupACategories";
            var json = await httpClient.GetStringAsync($"{gAApiUrl}?storeId={storeId}");
            return JsonConvert.DeserializeObject<List<GroupACategory>>(json);
        }

        public async Task<List<GroupBCategory>> GetGroupBFromStoreId(int storeId)
        {
            var httpClient = new HttpClient();
            var gBApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupBCategories";
            var json = await httpClient.GetStringAsync($"{gBApiUrl}?storeId={storeId}");
            return JsonConvert.DeserializeObject<List<GroupBCategory>>(json);
        }

        public async Task<List<GroupCCateory>> GetGroupCFromStoreId(int storeId)
        {
            var httpClient = new HttpClient();
            var gCApiUrl = "http://cookiehunterproject.azurewebsites.net/api/GroupCCategories";
            var json = await httpClient.GetStringAsync($"{gCApiUrl}?storeId={storeId}");
            return JsonConvert.DeserializeObject<List<GroupCCateory>>(json);
        }
        public async Task<List<Store>> GetStores()
        {
            var httpClient = new HttpClient();
            var storeApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Stores";
            var json = await httpClient.GetStringAsync($"{storeApiUrl}");
            return JsonConvert.DeserializeObject<List<Store>>(json);
        }

        public async Task<bool> RegisterUserHasRole(string email, string role, int storeId)
        {
            var userHasRole = new UserHasRole()
            {                                                       
                Email = email,
                Role = role,
                StoreId = storeId
            };
            var json = JsonConvert.SerializeObject(userHasRole);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uHRApiUrl = "http://cookiehunterproject.azurewebsites.net/api/UserHasRoles";
            var response = await httpClient.PostAsync(uHRApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterItem(string name, int storeId, int standardCategoryId) 
        {
            var item = new Item()
            {
                Name = name,
                StoreId = storeId,
                StandardCategoryId = standardCategoryId
            };
            var json = JsonConvert.SerializeObject(item);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var itemApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Items";
            var response = await httpClient.PostAsync(itemApiUrl, content);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RegisterCoupon(DateTime lastDate, int itemId, int standardCategoryId, int storeId, float price, int pointsWorth)
        {
            var coupon = new Coupon()
            {
                LastDate = lastDate,
                ItemId = itemId,
                StandardCategoryId = standardCategoryId,
                StoreId = storeId,
                Price = price,
                PointsWorth = pointsWorth
            };
            var json = JsonConvert.SerializeObject(coupon);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var couponApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Coupons";
            var response = await httpClient.PostAsync(couponApiUrl, content);

            return response.IsSuccessStatusCode;
        }
        public async Task<List<UserHasRole>> GetRolesFromEmail(string email)
        {
            var httpClient = new HttpClient();
            var uhrApiUrl = "http://cookiehunterproject.azurewebsites.net/api/UserHasRoles";
            var json = await httpClient.GetStringAsync($"{uhrApiUrl}?email={email}");
            return JsonConvert.DeserializeObject<List<UserHasRole>>(json);
        }

        public async Task<bool> RegisterPurchaseHistory(string userHasRoleEmail, int storeId, int points)
        {
            var purchaseHistory = new PurchaseHistory()
            {
                UserHasRoleEmail = userHasRoleEmail,
                StoreId = storeId,
                Points = points
            };
            var json = JsonConvert.SerializeObject(purchaseHistory);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var phApiUrl = "http://cookiehunterproject.azurewebsites.net/api/PurchaseHistories";
            var response = await httpClient.PostAsync(phApiUrl, content);

            return response.IsSuccessStatusCode;
        }
        public async Task<List<PurchaseHistory>> GetPurchaseHistoryFromEmailAndStoreId(int storeId, string userHasRoleEmail)
        {
            var httpClient = new HttpClient();
            var phApiUrl = "http://cookiehunterproject.azurewebsites.net/api/PurchaseHistories";
            var json = await httpClient.GetStringAsync($"{phApiUrl}?StoreId={storeId}&userhasroleemail={userHasRoleEmail}");
            return JsonConvert.DeserializeObject<List<PurchaseHistory>>(json);
        }
        public async Task<List<Coupon>> GetCouponFromStandardCategoryIdAndStoreId(int standardCategoryId, int storeId)
        {
            var httpClient = new HttpClient();
            var cApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Coupons";
            var json = await httpClient.GetStringAsync($"{cApiUrl}?standardCategoryId={standardCategoryId}&storeId={storeId}");
            return JsonConvert.DeserializeObject<List<Coupon>>(json);
        }

        public async Task<List<Item>> GetItemFromStoreId(int storeId)
        {
            var httpClient = new HttpClient();
            var iApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Items";
            var json = await httpClient.GetStringAsync($"{iApiUrl}?storeid={storeId}");
            return JsonConvert.DeserializeObject<List<Item>>(json);
        }
        public async Task<bool> RegisterUserHasSubscription(string userHasRoleEmail, int standardCategoryId, int storeId)
        {
            var userHasSubscription = new UserHasSubscription()
            {
                UserHasRoleEmail = userHasRoleEmail,
                StandardCategoryId = standardCategoryId,
                StoreId = storeId
            };
            var json = JsonConvert.SerializeObject(userHasSubscription);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uhsApiUrl = "http://cookiehunterproject.azurewebsites.net/api/UserHasSubscriptions";
            var response = await httpClient.PostAsync(uhsApiUrl, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Store>> GetUserHasSubscriptionsFromEmail(string userHasRoleEmail)
        {
            var httpClient = new HttpClient();
            var iApiUrl = "http://cookiehunterproject.azurewebsites.net/api/UserHasSubscriptions";
            var json = await httpClient.GetStringAsync($"{iApiUrl}?UserHasRoleEmail={userHasRoleEmail}");
            return JsonConvert.DeserializeObject<List<Store>>(json);
        }

        public async Task<List<Item>> GetItemFromItemId(int itemId)
        {
            var httpClient = new HttpClient();
            var iApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Items";
            var json = await httpClient.GetStringAsync($"{iApiUrl}/{itemId}");
            return JsonConvert.DeserializeObject<List<Item>>(json);
        }

        public async Task<List<Store>> GetStoreFromStoreId(int storeId)
        {
            var httpClient = new HttpClient();
            var sApiUrl = "http://cookiehunterproject.azurewebsites.net/api/Stores";
            var json = await httpClient.GetStringAsync($"{sApiUrl}/{storeId}");
            return JsonConvert.DeserializeObject<List<Store>>(json);
        }

        public async Task<List<StandardCategory>> GetStandardCategoryFromSCId(int scId)
        {
            var httpClient = new HttpClient();
            var scApiUrl = "http://cookiehunterproject.azurewebsites.net/api/StandardCategories";
            var json = await httpClient.GetStringAsync($"{scApiUrl}/{scId}");
            return JsonConvert.DeserializeObject<List<StandardCategory>>(json);
        }
    }


}
