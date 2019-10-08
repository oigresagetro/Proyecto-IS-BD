﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlCalidad.Models;

namespace ControlCalidad.Controllers
{
    public class localizationsController : Controller
    {
        private localizacoinesEntities db = new localizacoinesEntities();

        // GET: Province
        public List<SelectListItem> provinceList()
        {
            List<Provincia> provinces = db.Provincias.ToList();

            List<SelectListItem> provinceList = provinces.ConvertAll(province => { return new SelectListItem() {
                Text = province.nombre,
                Value = province.codigoPK.ToString(),
                Selected = false
                };
             });
            return provinceList;
        }


        public JsonResult cantonesList(int provincia)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Canton> cantonList = db.Cantons.Where(x => x.provinciaFK == provincia).ToList();
            return Json(cantonList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult districtsList(int provincia,int canton)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Distrito> cantonList = db.Distritoes.Where(x => x.cantonFK == canton && x.provinciaFK == provincia).ToList();
            return Json(cantonList, JsonRequestBehavior.AllowGet);


            //return new SelectList((from provincias in db.Provincias
            //                       select provincias.nombre).ToList());
        }

        public string provinceName(string strProvince)
        {
            int provinceIndex = System.Convert.ToInt32(strProvince);
            List<Provincia> provincia = db.Provincias.Where(x => x.codigoPK == provinceIndex).ToList();
            return provincia[0].nombre;
        }
        public string cantonName(string strProvince,string strCanton)
        {
            int province = System.Convert.ToInt32(strProvince);
            int cantonIndex = System.Convert.ToInt32(strCanton);
            List<Canton> canton = db.Cantons.Where(x => x.codigoPK == cantonIndex && x.provinciaFK == province).ToList();
            return canton[0].nombre;
        }
        public string districtName(string strProvince,  string strCanton, string strDistrict)
        {
            int province = System.Convert.ToInt32(strProvince);
            int canton = System.Convert.ToInt32(strCanton);
            int districtIndex = System.Convert.ToInt32(strDistrict);
            List<Distrito> distrito = db.Distritoes.Where(x => x.codigoPK == districtIndex && x.provinciaFK == province && x.cantonFK == canton).ToList();
            return distrito[0].nombre;
        }

        public SelectList TraerNombreCantones()
        { 
            return new SelectList((from cantones in db.Cantons
                                   select cantones.nombre).ToList());
        }

        public SelectList TraerNombreDistritos()
        {
            return new SelectList((from distritos in db.Distritoes
                                   select distritos.nombre).ToList());
        }


    }
}