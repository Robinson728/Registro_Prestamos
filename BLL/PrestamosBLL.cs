using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro_Prestamos.Models;
using Registro_Prestamos.DAL;

namespace Registro_Prestamos.BLL
{
    public class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamos)
        {
            if (!Existe(prestamos.PrestamoId))
                return Insertar(prestamos);
            else
                return Modificar(prestamos);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Prestamos.Any(e => e.PrestamoId == id);
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

        public static bool Insertar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Prestamos.Add(prestamos);
                paso = contexto.SaveChanges() > 0;
                AgregarBalance(prestamos);
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

        public static bool Modificar(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Entry(prestamos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
                AgregarBalance(prestamos);
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
            //Prestamos prestamos;
            try
            {
                var eliminado = contexto.Prestamos.Find(id);

                if (eliminado != null)
                {
                    contexto.Prestamos.Remove(eliminado);
                    paso = contexto.SaveChanges() > 0;
                    EliminarBalance(eliminado);
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

        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos prestamos;

            try
            {
                prestamos = contexto.Prestamos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamos;
        }

        public static bool AgregarBalance(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                prestamos.Balance = prestamos.Monto;

                contexto.Personas.Find(prestamos.PersonaId).Balance = prestamos.Balance;
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

        public static bool EliminarBalance(Prestamos prestamos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                //prestamos.Balance = prestamos.Monto;

                contexto.Personas.Find(prestamos.PersonaId).Balance -= prestamos.Balance;
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

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
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

        public static List<Prestamos> GetPrestamos()
        {
            Contexto contexto = new Contexto();
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = contexto.Prestamos.ToList();
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
