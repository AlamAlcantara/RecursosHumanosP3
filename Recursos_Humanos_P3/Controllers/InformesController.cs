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

        [HttpPost]
        public ActionResult Asignar_Encargado_Departamento()
        {
            int id_empleado = Convert.ToInt32(Request.Form["id_empleado"].ToString());
            int id_departamento = Convert.ToInt32(Request.Form["id_departamento"].ToString());

            Boolean existe = false;
            empleado e = db.empleado.Where(x => x.id == id_empleado).FirstOrDefault();

            if (e!=null)
            {
                existe = true;
            }

            if (existe == true)
            {

                departamento d = db.departamento.Where(x => x.id == id_departamento).FirstOrDefault();
                d.encargado = id_empleado;
                db.SaveChanges();

                List<departamento> departamentos = db.departamento.ToList();
                ViewData["departamentos"] = departamentos;
                return View("Departamentos");
            }
            else
            {
                List<departamento> departamentos = db.departamento.ToList();
                ViewData["departamentos"] = departamentos;
                return View("Departamentos");
            }
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult Cargos()
        {
            List<cargo> cargos = db.cargo.ToList();
            ViewData["cargos"] = cargos;
            return View();
        }

        public ActionResult Entrada_Empleados()
        {
            List<empleado> empleados = db.empleado.OrderBy(x => x.fecha_ingreso.Value.Month).ToList();
            ViewData["empleados"] = empleados;
            return View();
        }

        public ActionResult Buscar_Entrada_Mes()
        {
            int mes_entrada = 0;

            if (Request.Form["mes_entrada"] != null && Request.Form["mes_entrada"]!="")
            {
                mes_entrada = Convert.ToInt32(Request.Form["mes_entrada"]);
                List<empleado> empleados = db.empleado.Where(x => x.fecha_ingreso.Value.Month == mes_entrada).ToList();
                ViewData["empleados"] = empleados;
                return View("Entrada_Empleados");
            }
            else
            {
                return View("Entrada_Empleados");
            }  
        }

        public ActionResult Lista_Salida_Empleados()
        {
            List<salida_empleado> salidas = db.salida_empleado.OrderBy(x => x.fecha_salida).ToList();
            ViewData["salidas"] = salidas;
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Salida_Mes()
        {
            int mes = 0;

            if (Request.Form["mes_salida"]!=null && Request.Form["mes_salida"] !="")
            {
                mes = Convert.ToInt32(Request.Form["mes_salida"].ToString());
                List<salida_empleado> salidas = db.salida_empleado.Where(x => x.fecha_salida.Value.Month == mes).ToList();
                ViewData["salidas"] = salidas;
                return View("Lista_Salida_Empleados");
            }
            else
            {
                return View("Lista_Salida_Empleados");
            }
        }

        [HttpPost]
        public ActionResult Informacion_Empleado_Salida()
        {
            int id_empleado = Convert.ToInt32(Request.Form["id_empleado"].ToString());
            string fecha_salida = db.salida_empleado.Where(x => x.id_empleado == id_empleado).FirstOrDefault().fecha_salida.Value.Date.ToString("d");
            string fecha_entrada = db.empleado.Where(x => x.id == id_empleado).FirstOrDefault().fecha_ingreso.Value.Date.ToString("d");

            empleado e = db.empleado.Where(x => x.id == id_empleado).FirstOrDefault();

            ViewData["fecha_entrada"] = fecha_entrada;
            ViewData["fecha_salida"] = fecha_salida;
            return View(e);
        }

        public ActionResult Permisos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Salidas()
        {
            int id_empleado = 0;

            if (Request.Form["id_empleado"]!=null && Request.Form["id_empleado"] !="")
            {
                id_empleado = Convert.ToInt32(Request.Form["id_empleado"].ToString());
                empleado e = db.empleado.Where(x => x.id == id_empleado).FirstOrDefault();

                List<permiso> permisos = db.permiso.Where(x => x.id_empleado == id_empleado).ToList();
                ViewData["permisos"] = permisos;
                return View("Permisos", e);
            }
            else
            {
                return View("Permisos");
            }
        }
    }
}