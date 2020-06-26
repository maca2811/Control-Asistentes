using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace Servicios
{
	public class UsuariosServicios
	{

		#region variables globales
		private UsuarioDatos usuarioDatos = new UsuarioDatos();
		#endregion



		#region metodos
		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		/// <returns>list</returns>
		public List<Usuario> ObtenerUsuarios()
		{
			return usuarioDatos.ObtenerUsuarios();
		}

		/// <summary>
		///Mariela Calvo Méndez
		/// Abril/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		/// <returns>list</returns>
		public Usuario ObtenerUsuarioPorId(int idUsuario)
		{
			return usuarioDatos.ObtenerUsuarioPorID(idUsuario);
		}
		/// <summary>
		/// Jesús Torres R
		/// 27/03/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		public void insertarUsuarios(Usuario usuario)
		{
			usuarioDatos.insertarUsuarios(usuario);
		}


		/// <summary>
		/// Mariela Calvo Méndez
		/// Abril/2020
		/// Efecto: Edita los usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		public void editarUsuarios(Usuario usuario)
		{
			usuarioDatos.ActualizarUsuario(usuario);
		}

		/// <summary>
		/// Mariela Calvo Méndez
		/// Abril/2020
		/// Efecto: Edita los usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		public void eliminarUsuarios(Usuario usuario)
		{
			usuarioDatos.EliminarUsuario(usuario);
		}

		/// <summary>
		/// Mariela Calvo Méndez
		/// Mayo/2020
		/// Efecto: Regresa la lista de Usuarios 
		/// Requiere: -
		/// Modifica: -
		/// Devuelve: Lista de Usuarios
		/// </summary>
		/// <returns>list</returns>
		public Usuario ObtenerUsuarioAsistente(int idAsistente)
		{
			return usuarioDatos.ObtenerUsuarioAsistente(idAsistente);
		}
		#endregion
	}
}
