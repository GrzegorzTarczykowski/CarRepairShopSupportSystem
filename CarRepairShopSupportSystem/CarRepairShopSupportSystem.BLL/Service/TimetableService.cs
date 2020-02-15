using CarRepairShopSupportSystem.BLL.Enums;
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
    public class TimetableService : ITimetableService
    {
        private readonly IHttpClientService httpClientService;

        public TimetableService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public IEnumerable<Timetable> GetTimetableListPerDay(int year, int month, int day)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Timetable/GetPerDay?year={year}&month={month}&day={day}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Timetable> matchingTimetableList = JsonConvert.DeserializeObject<IEnumerable<Timetable>>(JsonContent);
                    if (matchingTimetableList != null)
                    {
                        return matchingTimetableList;
                    }
                }
                return Enumerable.Empty<Timetable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Timetable> GetTimetableListPerHour(int year, int month, int day, int hour)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Timetable/GetPerHour?year={year}&month={month}&day={day}&hour={hour}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Timetable> matchingTimetableList = JsonConvert.DeserializeObject<IEnumerable<Timetable>>(JsonContent);
                    if (matchingTimetableList != null)
                    {
                        return matchingTimetableList;
                    }
                }
                return Enumerable.Empty<Timetable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Timetable> GetTimetableListPerMonth(int year, int month)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Timetable/GetPerMonth?year={year}&month={month}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Timetable> matchingTimetableList = JsonConvert.DeserializeObject<IEnumerable<Timetable>>(JsonContent);
                    if (matchingTimetableList != null)
                    {
                        return matchingTimetableList;
                    }
                }
                return Enumerable.Empty<Timetable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Timetable> GetTimetableListPerYear(int year)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Timetable/GetPerYear?year={year}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Timetable> matchingTimetableList = JsonConvert.DeserializeObject<IEnumerable<Timetable>>(JsonContent);
                    if (matchingTimetableList != null)
                    {
                        return matchingTimetableList;
                    }
                }
                return Enumerable.Empty<Timetable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Timetable> GetTimetableListPerYearByUserId(int year, int userId)
        {
            try
            {
                HttpResponseMessage APIResponse = httpClientService.Get($"api/Timetable/GetPerYearByUserId?year={year}&userId={userId}");
                if (APIResponse.IsSuccessStatusCode)
                {
                    string JsonContent = APIResponse.Content.ReadAsStringAsync().Result;
                    IEnumerable<Timetable> matchingTimetableList = JsonConvert.DeserializeObject<IEnumerable<Timetable>>(JsonContent);
                    if (matchingTimetableList != null)
                    {
                        return matchingTimetableList;
                    }
                }
                return Enumerable.Empty<Timetable>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public OperationResult SaveTimetableForUser(Timetable timetable)
        {
            try
            {
                HttpResponseMessage tokenResponse = httpClientService.Post($"api/Timetable", timetable);

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
                return new OperationResult { ResultCode = ResultCode.Error, Message = "Wystąpił problem edycja zlecenia" };
            }
        }
    }
}
