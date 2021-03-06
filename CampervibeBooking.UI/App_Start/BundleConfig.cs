﻿using System.Web;
using System.Web.Optimization;

namespace CampervibeBooking.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/campervibebooking").Include(
                        "~/Scripts/Shared/Layout.js",
                        "~/Scripts/Shared/Datepicker.js",
                        "~/Scripts/Shared/Validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/Booking/Make").Include(
                        "~/Scripts/Booking/Make.js"));

            bundles.Add(new ScriptBundle("~/bundles/Customer/Register").Include(
                        "~/Scripts/Customer/Register.js"));

            bundles.Add(new StyleBundle("~/Content/campervibebooking").Include("~/Content/campervibebooking.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css"));
        }
    }
}