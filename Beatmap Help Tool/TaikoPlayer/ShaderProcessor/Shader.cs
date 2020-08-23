using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.TaikoPlayer.ShaderProcessor
{
    public class Shader : IDisposable
    {
        private readonly int handle;
        private bool disposedValue = false;

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexShaderSource;
            string fragmentShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            GL.CompileShader(vertexShader);

            string log = GL.GetShaderInfoLog(vertexShader);
            if (log != string.Empty)
                Console.WriteLine(log);

            GL.CompileShader(fragmentShader);

            log = GL.GetShaderInfoLog(vertexShader);
            if (log != string.Empty)
                Console.WriteLine(log);

            handle = GL.CreateProgram();

            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);

            GL.LinkProgram(handle);

            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        ~Shader()
        {
            GL.DeleteProgram(handle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(handle);
                disposedValue = true;
            }
        }

        public void Use()
        {
            GL.UseProgram(handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
