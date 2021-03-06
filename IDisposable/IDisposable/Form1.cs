﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// L198 renamed namespace, from IDisposable to IDisposableIO, because IDisposable is the same as the the namespace we inherit from
namespace IDisposableIO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // L197 binary reader instance, object
            System.IO.BinaryReader BR = new System.IO.BinaryReader(System.IO.File.OpenRead("C:\\Users\\judas\\HP DV 6000 source\\repos\\New test folder\\Test byte reader2.txt"));

            // reads two byte in hexadecimal little endian
            MessageBox.Show(BR.ReadUInt16().ToString("X"));

            // L197 use Dispose to delete it
            BR.Dispose();

            // L197 initialize MyClass constructor
            MyClass MC = new MyClass();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // L198 ceate instance using 'using' for MyClass2
            using (MyClass2 MC2 = new MyClass2())

            // L198 after executing everything within the squiggly brackets, Disposed of MyClass2
            {
                // L198 This will show because it is not in our Class2
                MessageBox.Show("closing MyClass2 using button2");
            }
        }
    }
    // L197 class
    class MyClass
    { 
        // L197 constructor
        public MyClass()
        {
            MessageBox.Show("MyClass contructor is initialized");
        }

        // L197 destructor
        ~MyClass()
        {
            MessageBox.Show("~MyClass destructor, closing", "Closing");
        }
       
    }

    // L198 class for button2, inherit from IDisposable
    class MyClass2 : IDisposable
    {
        // L198 declare image outside of constructor
        Image i;
        // L198 constructor
        public MyClass2()
        {
            i = Image.FromFile("C:\\Users\\judas\\HP DV 6000 source\\repos\\New test folder\\3D2A233CB79D060FA605B2484FDC8B2C.png");
        }

        // L198 this method does the removal of our MyClass2 class after a bool is passes thru
        protected virtual void Dispose(bool B)
        {
            // If bool is true, dispose of everything within squiggly bracket
            if(B)
            {
                // L198 dispose of i,  (Note: include any other objects, if you create some, within MyClass2)
                i.Dispose();
            }
        }

        // L198 method to be called to dispose of items in protected virtual void Remove
        public void Dispose()
        {
            // L198 pass Dispose true to the protected Remove method, so it will dispose of everything
            Dispose(true);

            // L198 surpress and finalize using the garbage protector method
            // this stops the program from executing content, in a decunstructor within this class
            GC.SuppressFinalize(this);
        }

        // L198 deconstructor example that will not execute at closing, because GC.SuppressFinalize will not allow this to execute
        // if GC.SuppressFinalize(this); is commented out, then the message will show when closing down the program
        ~MyClass2()
        {
            MessageBox.Show("MyClass2 closing");
        }
    }
}
