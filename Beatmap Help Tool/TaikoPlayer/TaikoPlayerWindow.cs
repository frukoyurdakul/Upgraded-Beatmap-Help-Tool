using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beatmap_Help_Tool.TaikoPlayer.KeyModel;
using Beatmap_Help_Tool.TaikoPlayer.ShaderProcessor;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Input;

namespace Beatmap_Help_Tool.TaikoPlayer
{
    public class TaikoPlayerWindow : GameWindow, IDisposable
    {
        private int gameplayPosition = 0;
        private int vertexBufferObject;
        private int vertexArrayObject;

        private readonly KeyProcessor keyProcessor;
        private Shader shader;
        private readonly float[] vertices =
        {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        public TaikoPlayerWindow() : base(640,
            480, GraphicsMode.Default,
            "Taiko Simultaneous Player")
        {
            keyProcessor = new KeyProcessor()
                .AssignKey(Key.Right, new OnKeyPressed(RightKeyPress))
                .AssignKey(Key.Left, new OnKeyPressed(LeftKeyPress))
                .AssignKey(Key.Space, new OnKeyPressed(ShiftKeyPress))
                .AssignKey(Key.Escape, new OnKeyPressed(EscKeyPress));

            GenerateShadersIfNecessary();
        }

        private void GenerateShadersIfNecessary()
        {
            if (!Directory.Exists("shaders"))
                Directory.CreateDirectory("shaders");
            if (!File.Exists("shaders/shader.frag"))
            {
                string text = @"
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}";
                File.WriteAllText("shaders/shader.frag", text);
            }
            if (!File.Exists("shaders/shader.vert"))
            {
                string text = @"
layout (location = 0) in vec3 aPosition;

void main()
{
	gl_Position = vec4(aPosition, 1.0);
}";
                File.WriteAllText("shaders/shader.vert", text);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0f, 0f, 0f, 1f);
            vertexBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();
            shader = new Shader("shaders/shader.vert", "shaders/shader.frag");
            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            keyProcessor.OnUpdateFrame();
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
            GL.DisableVertexAttribArray(0);
            shader.Dispose();
            base.OnUnload(e);
        }

        private void EscKeyPress()
        {
            Exit();
        }

        private void ShiftKeyPress()
        {
            throw new NotImplementedException();
        }

        private void LeftKeyPress()
        {
            throw new NotImplementedException();
        }

        private void RightKeyPress()
        {
            throw new NotImplementedException();
        }
    }
}
