﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Fat
{
   public  class Directory_Entry
    {
        public char[] File_name =  new char[11];
        public byte File_attribut;
        public byte[] File_empty = new byte[12];
        public int File_size;
        public int First_cluster;
        public byte [] Convert_byte(int value)
        {
            byte[] result = new byte[sizeof(int)];
            int[] value_arr = new int[1];
            value_arr[0] = value;
            Buffer.BlockCopy(value_arr, 0, result, 0, result.Length);
            return result;
        }
        public Directory_Entry()
        {  
        }
        public Directory_Entry(string File_name, byte File_attribut, int First_cluster, int File_size)
        {
            if (File_name.Length < 11)
            {
                for (int i = File_name.Length; i < 11; i++)
                {
                    File_name += " ";
                }
            
            }
             this.File_size = File_size;
          //this.File_name= File_name.ToCharArray() ;
             this.File_attribut = File_attribut;
             this.First_cluster = First_cluster;
            this.File_name = File_name.ToCharArray();
         
        }
        public byte[] Get_Bytes()
        {
            // public static string s = "asd";
            // public byte[] File_Name = Encoding.ASCII.GetBytes(s);
            byte[] dir_bytes = new byte[32];
            for (int i = 0; i < File_name.Length; i++)
            {
                dir_bytes[i] = (byte)File_name[i];
            }
            dir_bytes[11] = File_attribut;
            int j = 12;
            for (int i = 0; i <File_empty.Length; i++)
            {
                dir_bytes[j] = File_empty[i];
                j++;
            }
            byte[] first_cluster = BitConverter.GetBytes(First_cluster);
            for (int i = 0; i < first_cluster.Length; i++)
            {
                dir_bytes[j] = first_cluster[i];
                j++;
            }

            byte[] size = BitConverter.GetBytes(File_size);
            for (int i = 0; i < size.Length; i++)
            {
                dir_bytes[j] = size[i];
                j++;
            }
            return dir_bytes;
        }
        public Directory_Entry Get_Directory_Entry(byte[] bytes)   
        {
            char[] name = new char[11];
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = (char)bytes[i];
            }
            byte attr = bytes[11];
            byte[] empty = new byte[12];
            int j = 12;
            for (int i = 0; i < empty.Length; i++)
            {
                empty[i] = bytes[j];
                j++;
            }
            byte[] fc = new byte[4];
            for (int i = 0; i < fc.Length; i++)
            {
                fc[i] = bytes[j];
                j++;
            }
            int firstcluster = BitConverter.ToInt32(fc, 0);
            byte[] size = new byte[4];
            for (int i = 0; i < size.Length; i++)
            {
                size[i] = bytes[j];
                j++;
            }
            int filesize = BitConverter.ToInt32(size, 0);
            Directory_Entry DE = new Directory_Entry(new string(name), attr, firstcluster,File_size);
            DE.File_empty = empty;
            DE.File_size = filesize;
            return DE;
        }
        public Directory_Entry Get_Directory_Entry()
        {
            Directory_Entry op = new Directory_Entry(new string(this.File_name), this.File_attribut, this.First_cluster, this.File_size);
            return op;
        }
    }
}
