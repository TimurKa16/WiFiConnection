using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WiFiConnection
{
	public static class XmlConstructor
	{
		public static void WriteXmlFile(string name, string password, string filePath, string fileName)
		{
			string nameHex = null;

			for (int i = 0; i< name.Length; i++)
				 nameHex += String.Format("{0:X}", Convert.ToByte(name[i]));

			string xmlText = MakeXmlFile(name, nameHex, password);


			using (StreamWriter streamWriter = new StreamWriter(filePath + fileName))
            {
				streamWriter.Write(xmlText);
            }
		}

		private static string MakeXmlFile(string name, string nameHex, string password)
        {
			string xmlText =
"<?xml version = \"1.0\"?>" + Environment.NewLine +
"<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">" + Environment.NewLine +
			"\t" + "<name>" + name + "</name>" + Environment.NewLine +
			"\t" + "<SSIDConfig>" + Environment.NewLine +
				"\t\t" + "<SSID>" + Environment.NewLine +
				"\t\t\t" + "<hex>" + nameHex + "</hex>" + Environment.NewLine +
			   "\t\t\t" + "<name>" + name + "</name>" + Environment.NewLine +
		   "\t\t" + "</SSID>" + Environment.NewLine +
	   "\t" + "</SSIDConfig>" + Environment.NewLine +
	   "\t" + "<connectionType>ESS</connectionType>" + Environment.NewLine +
	   "\t" + "<connectionMode>auto</connectionMode>" + Environment.NewLine +
	   "\t" + "<MSM>" + Environment.NewLine +
		   "\t\t" + "<security>" + Environment.NewLine +
			   "\t\t\t" + "<authEncryption>" + Environment.NewLine +
				   "\t\t\t\t" + "<authentication>WPA2PSK</authentication>" + Environment.NewLine +
				   "\t\t\t\t" + "<encryption>AES</encryption>" + Environment.NewLine +
				   "\t\t\t\t" + "<useOneX>false</useOneX>" + Environment.NewLine +
			   "\t\t\t" + "</authEncryption>" + Environment.NewLine +
			   "\t\t\t" + "<sharedKey>" + Environment.NewLine +
				   "\t\t\t\t" + "<keyType>passPhrase</keyType>" + Environment.NewLine +
				   "\t\t\t\t" + "<protected>false</protected>" + Environment.NewLine +
					"\t\t\t\t" + "<keyMaterial>" + password + "</keyMaterial>" + Environment.NewLine +
				"\t\t\t" + "</sharedKey>" + Environment.NewLine +
			"\t\t" + "</security>" + Environment.NewLine +
		"\t" + "</MSM>" + Environment.NewLine +
"</WLANProfile>";
			return xmlText;
		}
	}
}
