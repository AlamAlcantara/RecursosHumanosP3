using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recursos_Humanos_P3.Controllers
{
    public class ProcesosController : Controller
    {
        RecursosHumanosP3Entities db = new RecursosHumanosP3Entities();
        // GET: Procesos
        public ActionResult CalculoNomina()
        {
           

            nomina n = new nomina();
            n.fecha = DateTime.Now;
            n.total = getTotalNomina();


            ViewData["cant_empleados"] = getTotalEmpleados();
            ViewData["error"]="";
            return View(n);
        }

        [HttpPost]
        public ActionResult Registrar_Nomina(nomina n)
        {
            string error = "false"; 
            if(ModelState.IsValid == false)
            {
                error = "true";
                ViewData["error"] = error;
                ViewData["cant_empleados"] = getTotalEmpleados();
                n.total = getTotalNomina();
                return View("CalculoNomina", n);
            }
            else
            {
                try
                {
                    db.nomina.Add(n);
                    db.SaveChanges();
                    ViewData["error"] = error;
                    n.total = getTotalNomina();
                    ViewData["cant_empleados"] = getTotalEmpleados();
                    return View("CalculoNomina", n);
                }
                catch (Exception e)
                {
                    error = "true";
                    ViewData["error"] = error;
                    ViewData["cant_empleados"] = getTotalEmpleados();
                    n.total = getTotalNomina();
                    return View("CalculoNomina");
                }
            }

        }

        public ActionResult SalidaEmpleado()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Codigo()
        {
            string codigo = Request.Form["codigo"].ToString();
            empleado em = db.empleado.Where(e => e.Codigo_Empleado == codigo).FirstOrDefault();
            ViewData["error"] = "";
            return View("SalidaEmpleado", em);
        }

        [HttpPost]
        public ActionResult Confirmar_Salida()
        {
           
            try
            {
                string causa = Request.Form["causa_salida"].ToString();
                salida_empleado salida = new salida_empleado();
                salida.tipo_salida = causa;
                salida.id_empleado = Convert.ToInt32(Request.Form["id_empleado_e"].ToString());
                salida.motivo = Request.Form["motivo_salida"].ToString();
                salida.fecha_salida = Convert.ToDateTime(Request.Form["fecha_salida"].ToString());

                db.salida_empleado.Add(salida);
                db.SaveChanges();
                ViewData["error"] = "false";
            }
            catch(Exception ex)
            {
                ViewData["error"] = "true ";
                ViewData["msj_error"] = " "+ ex.Message.ToString();
            }

            return View("SalidaEmpleado");
        }

        public decimal? getTotalNomina()
        {
            decimal? total = 0;
            var salarios = db.empleado.Where(e => e.estatus == "activo").ToList();

            foreach (var em in salarios)
            {
                if (em.salario != null)
                {
                    total += em.salario;
                }
            }

            return total;
        }

        public int getTotalEmpleados()
        {
            decimal? total = 0;
            var salarios = db.empleado.Where(e => e.estatus == "activo").ToList();

            foreach (var em in salarios)
            {
                if (em.salario != null)
                {
                    total += em.salario;
                }
            }

            return salarios.Count;
        }


        public ActionResult Vacacion()
        {
            ViewData["error"] = "";
            return View();
        }


        [HttpPost]
        public ActionResult Buscar_Empleado_Vacacion_Codigo()
        {

            string codigo = Request.Form["codigo"].ToString();
            empleado em = db.empleado.Where(e => e.Codigo_Empleado == codigo).FirstOrDefault();
            ViewData["error"] = "";

            if (em == null)
            {
                ViewData["msj_no_encontrado"] = "No se encontró ningún empleado con ese código";
            }

            return View("Vacacion",em);

        }

        [HttpPost]
        public ActionResult Registrar_Vacacion()
        {

            try
            {
                vacacion v = new vacacion();
                v.id_empleado = Convert.ToInt32(Request.Form["id_empleado"]);
                v.desde = Convert.ToDateTime(Request.Form["v_desde"]);
                v.hasta = Convert.ToDateTime(Request.Form["v_hasta"]);
                v.anio = Convert.ToInt32(Request.Form["v_anio"]);

                db.vacacion.Add(v);
                db.SaveChanges();
                ViewData["error"] = "false";

                return View("vacacion");

            }
            catch(Exception ex)
            {
                ViewData["error"] = "true";
                ViewData["msj_error"] = "Error: " + ex.Message.ToString();
                return View("vacacion");
            }

            
        }

        public ActionResult Permiso()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Permiso_Codigo()
        {

            string codigo = Request.Form["codigo"].ToString();
            empleado em = db.empleado.Where(e => e.Codigo_Empleado == codigo).FirstOrDefault();
            ViewData["error"] = "";

            if (em == null)
            {
                ViewData["msj_no_encontrado"] = "No se encontró ningún empleado con ese código";
            }

            return View("Permiso", em);

        }


        [HttpPost]
        public ActionResult Registrar_Permiso()
        {

            try
            {

                permiso p = new permiso();
                p.id_empleado = Convert.ToInt32(Request.Form["id_empleado"]);
                p.desde = Convert.ToDateTime(Request.Form["v_desde"]);
                p.hasta = Convert.ToDateTime(Request.Form["v_hasta"]);
                p.comentario = Request.Form["comentario"].ToString();

                db.permiso.Add(p);
                db.SaveChanges();
                ViewData["error"] = "false";

                return View("Permiso");

            }
            catch (Exception ex)
            {
                ViewData["error"] = "true";
                ViewData["msj_error"] = "Error: " + ex.Message.ToString();
                return View("Permiso");
            }

        }

    //----------------------------------------------------------------------------------------------------------------
        public ActionResult Licencia()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Licencia_Codigo()
        {

            string codigo = Request.Form["codigo"].ToString();
            empleado em = db.empleado.Where(e => e.Codigo_Empleado == codigo).FirstOrDefault();
            ViewData["error"] = "";

            if (em == null)
            {
                ViewData["msj_no_encontrado"] = "No se encontró ningún empleado con ese código";
            }

            return View("Licencia", em);

        }


        [HttpPost]
        public ActionResult Registrar_Licencia()
        {

            try
            {

                licencia l = new licencia();
                l.id_empleado = Convert.ToInt32(Request.Form["id_empleado"]);
                l.desde = Convert.ToDateTime(Request.Form["v_desde"]);
                l.hasta = Convert.ToDateTime(Request.Form["v_hasta"]);
                l.motivo = Request.Form["motivo"].ToString();
                l.comentario = Request.Form["comentario"].ToString();


                db.licencia.Add(l);
                db.SaveChanges();
                ViewData["error"] = "false";

                return View("Licencia");

            }
            catch (Exception ex)
            {
                ViewData["error"] = "true";
                ViewData["msj_error"] = "Error: " + ex.Message.ToString();
                return View("Licencia");
            }

        }

        //-----------------------------------------------------------------------------

        public ActionResult Editar_Empleado()
        {
            ViewData["departamentos"] = cargar_departamentos();
            ViewData["cargos"] = cargar_cargos();
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Buscar_Empleado_Editar()
        {
            string cod_empleado = "";
            

            if(Request.Form["cod_empleado"]!=null && Request.Form["cod_empleado"] != "")
            {
                cod_empleado = Request.Form["cod_empleado"].ToString();
                empleado e = db.empleado.Where(x => x.Codigo_Empleado == cod_empleado).FirstOrDefault();

                if (e != null)
                {
                    ViewData["departamentos"] = cargar_departamentos();
                    ViewData["cargos"] = cargar_cargos();
                    ViewData["error"] = "";
                    return View("Editar_Empleado", e);
                }
                else
                {
                    ViewData["departamentos"] = cargar_departamentos();
                    ViewData["cargos"] = cargar_cargos();
                    ViewData["error"] = "";
                    return View("Editar_Empleado");
                }

                
            }
            else
            {
                return View("Editar_Empleado");
            }

            
        }

        [HttpPost]
        public ActionResult Actualizar_Empleado(empleado e)
        {
            try
            {
                string cod = Request.Form["cod_empleado"].ToString();
                empleado em = db.empleado.Where(x => x.Codigo_Empleado == cod).FirstOrDefault();


                em.Nombre = Request.Form["nombre"].ToString();
                em.Apellido = Request.Form["apellido"].ToString();
                em.salario = Convert.ToDecimal(Request.Form["salario"]);
                em.fecha_ingreso = Convert.ToDateTime(Request.Form["fecha_ingreso"]);
                em.id_departamento = Convert.ToInt32(Request.Form["departamentoselect"].ToString());
                em.id_cargo = Convert.ToInt32(Request.Form["cargoselect"].ToString());
                em.telefono = Request.Form["telefono"].ToString();
                em.estatus = Request.Form["estatus"].ToString();

                db.SaveChanges();

                ViewData["departamentos"] = cargar_departamentos();
                ViewData["cargos"] = cargar_cargos();
                ViewData["error"] = "false";
                return View("Editar_Empleado");
            }
            catch (Exception ex)
            {
                ViewData["departamentos"] = cargar_departamentos();
                ViewData["cargos"] = cargar_cargos();
                ViewData["error"] = "true";
                return View("Editar_Empleado");
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