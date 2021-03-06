﻿using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShopSupportSystem.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService httpClientService;
        private readonly IApplicationSessionService applicationSessionService;

        public UserService(IHttpClientService httpClientService, IApplicationSessionService applicationSessionService)
        {
            this.httpClientService = httpClientService;
            this.applicationSessionService = applicationSessionService;
        }

        public OperationResult AddUser(User user)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post("api/User", user);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    return new OperationResult { ResultCode = ResultCode.Successful };
                }

                OperationResult operationResult = JsonConvert.DeserializeObject<OperationResult>(tokenResponse.Content.ReadAsStringAsync().Result);
                operationResult.ResultCode = ResultCode.Error;
                return operationResult;
            }
            catch (Exception)
            {
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z rejestracją" };
            }
        }

        public OperationResult EditUser(User user)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Put($"api/User/{user.UserId}", user);

                if (tokenResponse.IsSuccessStatusCode)
                {
                    return new OperationResult { ResultCode = ResultCode.Successful };
                }

                OperationResult operationResult = JsonConvert.DeserializeObject<OperationResult>(tokenResponse.Content.ReadAsStringAsync().Result);
                operationResult.ResultCode = ResultCode.Error;
                return operationResult;
            }
            catch (Exception)
            {
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem z edycja użytkownika" };
            }
        }

        public IEnumerable<User> GetAllUserList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/User");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<User> matchingUserList = JsonConvert.DeserializeObject<IEnumerable<User>>(JsonContent);
                    if (matchingUserList != null)
                    {
                        return matchingUserList;
                    }
                }
                return Enumerable.Empty<User>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> GetAllWorkerList()
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/User/GetAllWorkerList");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<User> matchingUserList = JsonConvert.DeserializeObject<IEnumerable<User>>(JsonContent);
                    if (matchingUserList != null)
                    {
                        return matchingUserList;
                    }
                }
                return Enumerable.Empty<User>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetUser(string username, string password)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/User?username={username}&password={password}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    User matchingUser = JsonConvert.DeserializeObject<User>(JsonContent);
                    if (matchingUser != null)
                    {
                        applicationSessionService.AddUserIntoApplicationSession(matchingUser);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
