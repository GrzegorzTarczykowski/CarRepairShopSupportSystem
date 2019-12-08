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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "VehiclePartListActivity")]
    public class VehiclePartListActivity : AppCompatActivity
    {
        private readonly IVehiclePartService vehiclePartService;
        private IList<VehiclePart> vehiclePartList;
        private IList<VehiclePart> selectedVehiclePartList;

        public VehiclePartListActivity()
        {
            vehiclePartService = new VehiclePartService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehiclePartList);
            GridView gvVehiclePartList = FindViewById<GridView>(Resource.Id.gvVehiclePartList);
            RefreshGvVehiclePartList(gvVehiclePartList);
            gvVehiclePartList.ItemClick += GvVehiclePartList_ItemClick;
            FindViewById<Button>(Resource.Id.btnAddVehiclePart).Click += BtnAddVehiclePart_Click;
            FindViewById<Button>(Resource.Id.btnSubmitSelectedVehicleParts).Click += BtnSubmitSelectedVehicleParts_Click;
        }

        private void RefreshGvVehiclePartList(GridView gvVehiclePartList)
        {
            vehiclePartList = vehiclePartService.GetAllVehiclePartList().ToList();
            selectedVehiclePartList = JsonConvert.DeserializeObject<IList<VehiclePart>>(Intent.GetStringExtra("SelectedVehiclePartList")) ?? new List<VehiclePart>();
            gvVehiclePartList.Adapter = new VehiclePartAdapter(this, vehiclePartList.ToArray(), selectedVehiclePartList.ToArray());
        }

        private void GvVehiclePartList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            VehiclePart vehiclePart = selectedVehiclePartList.FirstOrDefault(svp => svp.VehiclePartId == vehiclePartList[e.Position].VehiclePartId);
            if (vehiclePart != null)
            {
                ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.Transparent);
                selectedVehiclePartList.Remove(vehiclePart);
            }
            else
            {
                ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.GreenYellow);
                selectedVehiclePartList.Add(vehiclePartList[e.Position]);
            }
        }

        private void BtnSubmitSelectedVehicleParts_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OrderEditorActivity));
            intent.PutExtra("SelectedVehiclePartList", JsonConvert.SerializeObject(selectedVehiclePartList));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void BtnAddVehiclePart_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}