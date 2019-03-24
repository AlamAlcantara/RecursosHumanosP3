using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recursos_Humanos_P3.Controllers
{
    public class MantenimientoController : Controller
    {
        RecursosHumanosP3Entities db = new RecursosHumanosP3Entities();
        // GET: Mantenimiento
        public ActionResult Empleado()
        {
            
            ViewData["departamentos"] = cargar_departamentos();
            ViewData["cargos"] = cargar_cargos();
            ViewData["error"] = "";
            return View();
        }

        public ActionResult Departamento()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registrar_Departamento(departamento dept)
        {


            //verificar si el codigo del empleado ya existe
            var cods = from r in db.departamento
                       select r.Codigo_Departamento;


            if (cods.Contains(dept.Codigo_Departamento))
            {
                ViewData["registro_existente"] = "El codigo del departamento ya existe!";
                ViewData["error"] = "true";
                return View("Departamento", dept);
            }
            else if (ModelState.IsValid == false)
            {
                ViewData["error"] = "true";
                return View("Departamento", dept);
            }
            else
            {
                db.departamento.Add(dept);
                db.SaveChanges();
                ViewData["error"] = "false";
                return View("Departamento");
            }

        }

        public ActionResult Cargo()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registrar_Cargo(cargo c)
        {
            if (ModelState.IsValid == false)
            {
                ViewData["error"] = "true";
                return View("Cargo", c);
            }
            else
            {
                db.cargo.Add(c);
                db.SaveChanges();
                ViewData["error"] = "false";
                return View("Cargo");
            }

        }

        [HttpPost]
        public ActionResult Registrar_Empleado (empleado e)
        {

            e.estatus = Request.Form["estatus"].ToString();

            e.id_cargo = Convert.ToInt32(Request.Form["cargoselect"]);
            e.id_departamento = Convert.ToInt32(Request.Form["departamentoselect"]);

             var cargos = from l in db.cargo
                          where l.id == e.id_cargo
                          select l.id;

             int idc = cargos.FirstOrDefault();
             cargo ca = db.cargo.Where(i=>i.id == idc).FirstOrDefault();
             e.cargo = ca;

            //verificar si el codigo del empleado ya existe
            var cods = from r in db.empleado
                       select r.Codigo_Empleado;


            if (cods.Contains(e.Codigo_Empleado))
            {
                ViewData["registro_existente"] = "El codigo del empleado ya existe!";
                ViewData["error"] = "true";
                ViewData["departamentos"] = cargar_departamentos();
                ViewData["cargos"] = cargar_cargos();
                return View("Empleado", e);
            }else if(ModelState.IsValid == false)
            {
                ViewData["error"] = "true";
                ViewData["departamentos"] = cargar_departamentos();
                ViewData["cargos"] = cargar_cargos();
                return View("Empleado",e);
            }
            else
            {
                db.empleado.Add(e);
                db.SaveChanges();
                ViewData["error"] = "false";
                ViewData["departamentos"] = cargar_departamentos();
                ViewData["cargos"] = cargar_cargos();
                return View("Empleado");
            }

        }


        public List<cargo> cargar_cargos()
        {
            List<cargo> cargoslist = new List<cargo>();

            foreach (cargo c in db.cargo)
            {
                cargoslist.Add(c);
            }

            return cargoslist;
        }

        public List<departamento> cargar_departamentos()
        {
            List<departamento> departamentoslist = new List<departamento>();

            foreach (departamento d in db.departamento)
            {
                departamentoslist.Add(d);
            }

            return departamentoslist;
        }

    }
}