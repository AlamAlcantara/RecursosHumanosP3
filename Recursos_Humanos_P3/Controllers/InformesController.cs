using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recursos_Humanos_P3.Controllers
{
    public class InformesController : Controller
    {
        RecursosHumanosP3Entities db = new RecursosHumanosP3Entities();
        // GET: Informes
        public ActionResult Nominas()
        {
            List<nomina> nominas = db.nomina.ToList();
            ViewData["nominas"] = nominas;

            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Nominas()
        {

            int anio=0;
            int mes=0;

            if (Request.Form["anio_nomina"].ToString()!=null && Request.Form["anio_nomina"].ToString()!="")
            {
                anio = Convert.ToInt32(Request.Form["anio_nomina"].ToString());
            }
            if (Request.Form["mes_nomina"].ToString() != null && Request.Form["mes_nomina"].ToString() != "mes" && Request.Form["mes_nomina"].ToString() != "")
            {
                 mes = Convert.ToInt32(Request.Form["mes_nomina"].ToString());
            }

            int pr = 0;
            if (anio!=null && anio!=0)
            {
                List<nomina> nominas = db.nomina.Where(x => x.fecha.Value.Year == anio).ToList();
                ViewData["nominas"] = nominas;
                return View("Nominas");
            }else if (mes!=null && mes!=0)
            {
                List<nomina> nominas = db.nomina.Where(x => x.fecha.Value.Month == mes).ToList();
                ViewData["nominas"] = nominas;
                return View("Nominas");
            }else if(anio != null && anio!=0 && mes != null && mes !=null && mes !=0)
            {
                List<nomina> nominas = db.nomina.Where(x => x.fecha.Value.Month == mes && x.fecha.Value.Year == anio).ToList();
                ViewData["nominas"] = nominas;
                return View("Nominas");
            }
            else
            {
                return View("Nominas");
            }

        }

        //---------------------------------------------------------------------------------------------------------------

        public ActionResult Empleados_Activos()
        {
            List<empleado> empleados = db.empleado.Where(x=>x.estatus=="activo").ToList();
            ViewData["empleados"] = empleados;
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Activo()
        {
            string nombre_empleado = "";
            string nombre_departamento = "";

            if (Request.Form["nombre_empleado"]!=null && Request.Form["nombre_empleado"] != "")
            {
                nombre_empleado = Request.Form["nombre_empleado"].ToString();
            }
            if (Request.Form["nombre_departamento"] != null && Request.Form["nombre_departamento"] != "")
            {
                nombre_departamento = Request.Form["nombre_departamento"].ToString();
            }


            if (nombre_empleado!="")
            {
                List<empleado> empleados = db.empleado.Where(x => x.Nombre == nombre_empleado && x.estatus=="activo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Activos");

            }else if (nombre_departamento!="")
            {
                List<empleado> empleados = db.empleado.Where(x => x.departamento.Nombre == nombre_departamento && x.estatus == "activo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Activos");
            }else if (nombre_empleado != "" && nombre_departamento != "")
            {
                List<empleado> empleados = db.empleado.Where(x => x.departamento.Nombre == nombre_departamento && x.Nombre == nombre_empleado && x.estatus == "activo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Activos");
            }
            else
            {
                return View("Empleados_Activos");
            }    
        }

        public ActionResult Empleados_Inactivos()
        {
            List<empleado> empleados = db.empleado.Where(x => x.estatus == "inactivo").ToList();
            ViewData["empleados"] = empleados;
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Inactivos()
        {
            string nombre_empleado = "";
            string nombre_departamento = "";

            if (Request.Form["nombre_empleado"] != null && Request.Form["nombre_empleado"] != "")
            {
                nombre_empleado = Request.Form["nombre_empleado"].ToString();
            }
            if (Request.Form["nombre_departamento"] != null && Request.Form["nombre_departamento"] != "")
            {
                nombre_departamento = Request.Form["nombre_departamento"].ToString();
            }


            if (nombre_empleado != "")
            {
                List<empleado> empleados = db.empleado.Where(x => x.Nombre == nombre_empleado && x.estatus == "inactivo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Inactivos");

            }
            else if (nombre_departamento != "")
            {
                List<empleado> empleados = db.empleado.Where(x => x.departamento.Nombre == nombre_departamento && x.estatus == "inactivo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Inactivos");
            }
            else if (nombre_empleado != "" && nombre_departamento != "")
            {
                List<empleado> empleados = db.empleado.Where(x => x.departamento.Nombre == nombre_departamento && x.Nombre == nombre_empleado && x.estatus == "inactivo").ToList();
                ViewData["empleados"] = empleados;
                return View("Empleados_Inactivos");
            }
            else
            {
                return View("Empleados_Inactivos");
            }
        }

        public ActionResult Departamentos()
        {
            List<departamento> departamentos = db.departamento.ToList();
            ViewData["departamentos"] = departamentos;
            return View();
        }

    }
}