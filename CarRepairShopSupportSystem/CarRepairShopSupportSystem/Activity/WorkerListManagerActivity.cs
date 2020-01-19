using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Wybór pracownika")]
    public class WorkerListManagerActivity : AppCompatActivity
    {
        private const int workerManagerRequestCode = 1;
        private readonly IUserService userService;
        private IList<User> workerList;
        private IList<User> selectedWorkerList;

        public WorkerListManagerActivity()
        {
            userService = new UserService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_workerListManager);
            GridView gvWorkerList = FindViewById<GridView>(Resource.Id.gvWorkerList);
            RefreshGvWorkerList(gvWorkerList);
            gvWorkerList.ItemClick += GvWorkerList_ItemClick;
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Button btnAddWorker = FindViewById<Button>(Resource.Id.btnAddWorker);
                btnAddWorker.Click += BtnAddWorker_Click;
                btnAddWorker.Visibility = ViewStates.Visible;
            }
            else
            {
                Button btnSubmitSelectedWorker = FindViewById<Button>(Resource.Id.btnSubmitSelectedWorker);
                btnSubmitSelectedWorker.Click += BtnSubmitSelectedWorker_Click;
                btnSubmitSelectedWorker.Visibility = ViewStates.Visible;
            }
        }

        private void BtnAddWorker_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(RegisterActivity));
            nextActivity.PutExtra(nameof(OperationType), OperationType.Add.GetDescription());
            StartActivityForResult(nextActivity, workerManagerRequestCode);
        }

        private void BtnSubmitSelectedWorker_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OrderDetailsActivity));
            intent.PutExtra("SelectedWorkerList", JsonConvert.SerializeObject(selectedWorkerList));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void GvWorkerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Intent nextActivity = new Intent(this, typeof(RegisterActivity));
                nextActivity.PutExtra(nameof(OperationType), OperationType.Edit.GetDescription());
                nextActivity.PutExtra("SelectedWorker", JsonConvert.SerializeObject(workerList[e.Position]));
                StartActivity(nextActivity);
                StartActivityForResult(nextActivity, workerManagerRequestCode);
            }
            else
            {
                User user = selectedWorkerList.FirstOrDefault(sw => sw.UserId == workerList[e.Position].UserId);
                if (user != null)
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    selectedWorkerList.Remove(user);
                }
                else
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.GreenYellow);
                    selectedWorkerList.Add(workerList[e.Position]);
                }
            }
        }

        private void RefreshGvWorkerList(GridView gvWorkerList)
        {
            workerList = userService.GetAllWorkerList().ToList();
            selectedWorkerList = JsonConvert.DeserializeObject<IList<User>>(Intent.GetStringExtra("SelectedWorkerList") ?? string.Empty) ?? new List<User>();
            gvWorkerList.Adapter = new UserAdapter(this, workerList.ToArray(), selectedWorkerList.ToArray());
        }
    }
}