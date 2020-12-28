using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.Extensions;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista części")]
    public class VehiclePartListActivity : AppCompatActivity
    {
        private const int vehiclePartRequestCode = 1;
        private readonly IVehiclePartService vehiclePartService;
        private List<VehiclePart> vehiclePartList;
        private List<VehiclePart> selectedVehiclePartList;

        public VehiclePartListActivity()
        {
            vehiclePartService = MainApplication.Container.Resolve<IVehiclePartService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehiclePartList);
            ListView lvVehiclePartList = FindViewById<ListView>(Resource.Id.lvVehiclePartList);
            RefreshGvVehiclePartList(lvVehiclePartList);
            lvVehiclePartList.ChoiceMode = ChoiceMode.Multiple;
            lvVehiclePartList.ItemClick += LvVehiclePartList_ItemClick;
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Button btnAddVehiclePart = FindViewById<Button>(Resource.Id.btnAddVehiclePart);
                btnAddVehiclePart.Click += BtnAddVehiclePart_Click;
                btnAddVehiclePart.Visibility = ViewStates.Visible;
            }
            else
            {
                Button btnSubmitSelectedVehicleParts = FindViewById<Button>(Resource.Id.btnSubmitSelectedVehicleParts);
                btnSubmitSelectedVehicleParts.Click += BtnSubmitSelectedVehicleParts_Click;
                btnSubmitSelectedVehicleParts.Visibility = ViewStates.Visible;
            }
        }

        private void RefreshGvVehiclePartList(ListView lvVehiclePartList)
        {
            vehiclePartList = vehiclePartService.GetAllVehiclePartList().ToList();
            selectedVehiclePartList = JsonConvert.DeserializeObject<List<VehiclePart>>(Intent.GetStringExtra("SelectedVehiclePartList") ?? string.Empty) ?? new List<VehiclePart>();
            lvVehiclePartList.Adapter = new VehiclePartAdapter(this, vehiclePartList);
            selectedVehiclePartList.ForEach(selectedVehiclePart =>
            {
                lvVehiclePartList.SetItemChecked(vehiclePartList.FindIndex(vp => vp.VehiclePartId == selectedVehiclePart.VehiclePartId), true);
            });
        }

        private void LvVehiclePartList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Intent nextActivity = new Intent(this, typeof(VehiclePartActivity));
                nextActivity.PutExtra("SelectedVehiclePart", JsonConvert.SerializeObject(vehiclePartList[e.Position]));
                StartActivityForResult(nextActivity, vehiclePartRequestCode);
            }
        }

        private void BtnSubmitSelectedVehicleParts_Click(object sender, EventArgs e)
        {
            selectedVehiclePartList
                = FindViewById<ListView>(Resource.Id.lvVehiclePartList).GetSelectedItems<VehiclePart>();
            Intent intent = new Intent(this, typeof(OrderEditorActivity));
            intent.PutExtra("SelectedVehiclePartList", JsonConvert.SerializeObject(selectedVehiclePartList));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void BtnAddVehiclePart_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehiclePartActivity));
            StartActivityForResult(nextActivity, vehiclePartRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == vehiclePartRequestCode)
            {
                ListView lvVehiclePartList = FindViewById<ListView>(Resource.Id.lvVehiclePartList);
                RefreshGvVehiclePartList(lvVehiclePartList);
            }
        }
    }
}