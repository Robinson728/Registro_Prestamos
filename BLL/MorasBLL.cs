using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registro_Prestamos.Models;
using Registro_Prestamos.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Registro_Prestamos.BLL
{
    public class MorasBLL
    {
        public static bool Guardar(Moras moras)
        {
            if (!Existe(moras.MoraId))
                return Insertar(moras);
            else
                return Modificar(moras);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Moras.Any(e => e.MoraId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool Insertar(Moras moras)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Moras.Add(moras);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Moras moras)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Entry(moras).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                var eliminado = contexto.Moras.Find(id);

                if (eliminado != null)
                {
                    contexto.Moras.Remove(eliminado);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Moras Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Moras moras;

            try
            {
                moras = contexto.Moras.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return moras;
        }

        public static List<Moras> GetList(Expression<Func<Moras, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Moras> lista = new List<Moras>();

            try
            {
                lista = contexto.Moras.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Moras> GetMoras()
        {
            Contexto contexto = new Contexto();
            List<Moras> lista = new List<Moras>();

            try
            {
                lista = contexto.Moras.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
