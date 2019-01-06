/*
 * Created by SharpDevelop.
 * User: Bob (XuYong Hou) houxuyong@hotmail.com
 * Date: 2018/12/26
 * Time: 19:50
 * 
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XCmetaUpdate
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		string xml_FilePath = "";
		string xml_FilePath_1 = "";
		
		string xml_FilePath_2 = "";
		
		/// <summary>
		/// xml 2 json
		/// https://blog.csdn.net/cwj649956781/article/details/22741181
		/// </summary>
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/// <summary>
		/// https://blog.csdn.net/yongh701/article/details/70766187 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnCategoryLoadClick(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();//一个打开文件的对话框
            openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件  
            {
                xml_FilePath = openFileDialog1.FileName;//记录用户选择的文件路径
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
                xmlDocument.Load(xml_FilePath);//载入路径这个xml
                try
                {
                    XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("cmeta").ChildNodes;//选择class为根结点并得到旗下所有子节点
                    dgvCatagory.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                    foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                        //旗下的子节点<name>和<number>分别放入dataGridView1
                        int index = dgvCatagory.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                        dgvCatagory.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                        dgvCatagory.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对！");
                }
            }
            else
            {
                MessageBox.Show("请打开XML文件");
            }
		}
		
		void BtnCategoryWriteClick(object sender, EventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {
                xmlDocument.Load(xml_FilePath);
                XmlNode xmlElement_cmeta = xmlDocument.SelectSingleNode("cmeta");//找到<cmeta>作为根节点
                xmlElement_cmeta.RemoveAll();//删除旗下所有节点
                int row = dgvCatagory.Rows.Count;//得到总行数    
                int cell = dgvCatagory.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row - 1; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_category = xmlDocument.CreateElement("category");
                        
                    XmlElement xmlElement_mid = xmlDocument.CreateElement("mid");//创建一个<mid>节点cmet
                	XmlElement xmlElement_name = xmlDocument.CreateElement("name");//创建<name>节点
                	
                	xmlElement_mid.InnerText = dgvCatagory.Rows[i].Cells[0].Value.ToString();
               		xmlElement_name.InnerText = dgvCatagory.Rows[i].Cells[1].Value.ToString();
               		
               		xmlElement_category.AppendChild(xmlElement_mid);
					xmlElement_category.AppendChild(xmlElement_name);
      
					xmlElement_cmeta.AppendChild(xmlElement_category);
                }
                xmlDocument.Save(xml_FilePath);//保存这个xml
            }
            else//如果用户未读入xml文件，我们的任务就新建一个xml文件了
            {
                SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框
                saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径
                {
                    XmlElement xmlElement_cmeta = xmlDocument.CreateElement("cmeta");//创建一个<class>节点
                    int row = dgvCatagory.Rows.Count;//得到总行数    
                    //int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数    
                    for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
                    {
                    	XmlElement xmlElement_category = xmlDocument.CreateElement("category");
                        
                        XmlElement xmlElement_mid = xmlDocument.CreateElement("mid");//创建一个<mid>节点cmet
                    	XmlElement xmlElement_name = xmlDocument.CreateElement("name");//创建<name>节点
                    	
                    	xmlElement_mid.InnerText = dgvCatagory.Rows[i].Cells[0].Value.ToString();
                   		xmlElement_name.InnerText = dgvCatagory.Rows[i].Cells[1].Value.ToString();
                   		
                   		xmlElement_category.AppendChild(xmlElement_mid);
						xmlElement_category.AppendChild(xmlElement_name);
      
						xmlElement_cmeta.AppendChild(xmlElement_category);
                    }
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", ""));//编写文件头
                    xmlDocument.AppendChild(xmlElement_cmeta);//将这个<class>附到总文件头，而且设置为根结点
                    xmlDocument.Save(saveFileDialog1.FileName);//保存这个xml文件
                }
                else
                {
                    MessageBox.Show("请保存为XML文件");
                }
            }
		}
		void TabPage2Click(object sender, EventArgs e)
		{
	
		}
		void BtnActivityLoadClick(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();//一个打开文件的对话框
            openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件  
            {
                xml_FilePath_1 = openFileDialog1.FileName;//记录用户选择的文件路径
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
                xmlDocument.Load(xml_FilePath_1);//载入路径这个xml
                try
                {
                    XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("cmeta").ChildNodes;//选择class为根结点并得到旗下所有子节点
                    dgvActivity.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                    foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                        //旗下的子节点<name>和<number>分别放入dataGridView1
                        int index = dgvActivity.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                        dgvActivity.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                        dgvActivity.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                         dgvActivity.Rows[index].Cells[2].Value = xmlElement.ChildNodes.Item(2).InnerText;
                          dgvActivity.Rows[index].Cells[3].Value = xmlElement.ChildNodes.Item(3).InnerText;
                           dgvActivity.Rows[index].Cells[4].Value = xmlElement.ChildNodes.Item(4).InnerText;
                            dgvActivity.Rows[index].Cells[5].Value = xmlElement.ChildNodes.Item(5).InnerText;
                             dgvActivity.Rows[index].Cells[6].Value = xmlElement.ChildNodes.Item(6).InnerText;
                    } 
                }
                catch
                {
                    MessageBox.Show("XML格式不对！");
                }
            }
            else
            {
                MessageBox.Show("请打开XML文件");
            }
		}
		void BtnActivityWriteClick(object sender, EventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath_1 != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {
                xmlDocument.Load(xml_FilePath_1);
                XmlNode xmlElement_cmeta = xmlDocument.SelectSingleNode("cmeta");//找到<cmeta>作为根节点
                xmlElement_cmeta.RemoveAll();//删除旗下所有节点
                int row = dgvActivity.Rows.Count;//得到总行数    
                int cell = dgvActivity.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row - 1; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_activity = xmlDocument.CreateElement("activity");
                        
                    XmlElement xmlElement_mid = xmlDocument.CreateElement("mid");//创建一个<mid>节点cmet
                	XmlElement xmlElement_aid = xmlDocument.CreateElement("aid");//创建<name>节点
                	XmlElement xmlElement_aname = xmlDocument.CreateElement("aname");//创建<name>节点
                	XmlElement xmlElement_target = xmlDocument.CreateElement("target");//创建<name>节点
                	XmlElement xmlElement_content = xmlDocument.CreateElement("content");//创建<name>节点
                	XmlElement xmlElement_duration = xmlDocument.CreateElement("duration");//创建<name>节点
                	XmlElement xmlElement_desc = xmlDocument.CreateElement("desc");//创建<name>节点
                	
                	xmlElement_mid.InnerText = dgvActivity.Rows[i].Cells[0].Value.ToString();
               		xmlElement_aid.InnerText = dgvActivity.Rows[i].Cells[1].Value.ToString();
               		xmlElement_aname.InnerText = dgvActivity.Rows[i].Cells[2].Value.ToString();
               		xmlElement_target.InnerText = dgvActivity.Rows[i].Cells[3].Value.ToString();
               		xmlElement_content.InnerText = dgvActivity.Rows[i].Cells[4].Value.ToString();
               		xmlElement_duration.InnerText = dgvActivity.Rows[i].Cells[5].Value.ToString();
               		xmlElement_desc.InnerText = dgvActivity.Rows[i].Cells[6].Value.ToString();
               		
               		xmlElement_activity.AppendChild(xmlElement_mid);
					xmlElement_activity.AppendChild(xmlElement_aid);
					xmlElement_activity.AppendChild(xmlElement_aname);
					xmlElement_activity.AppendChild(xmlElement_target);
					xmlElement_activity.AppendChild(xmlElement_content);
					xmlElement_activity.AppendChild(xmlElement_duration);
					xmlElement_activity.AppendChild(xmlElement_desc);
      
					xmlElement_cmeta.AppendChild(xmlElement_activity);
                }
                xmlDocument.Save(xml_FilePath_1);//保存这个xml
            }
            else//如果用户未读入xml文件，我们的任务就新建一个xml文件了
            {
                SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框
                saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径
                {
                    XmlElement xmlElement_cmeta = xmlDocument.CreateElement("cmeta");//创建一个<class>节点
                    int row = dgvActivity.Rows.Count;//得到总行数    
                    //int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数    
                    for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
                    {
                    	XmlElement xmlElement_activity = xmlDocument.CreateElement("activity");
                        
                    XmlElement xmlElement_mid = xmlDocument.CreateElement("mid");//创建一个<mid>节点cmet
                	XmlElement xmlElement_aid = xmlDocument.CreateElement("aid");//创建<name>节点
                	XmlElement xmlElement_aname = xmlDocument.CreateElement("aname");//创建<name>节点
                	XmlElement xmlElement_target = xmlDocument.CreateElement("target");//创建<name>节点
                	XmlElement xmlElement_content = xmlDocument.CreateElement("content");//创建<name>节点
                	XmlElement xmlElement_duration = xmlDocument.CreateElement("duration");//创建<name>节点
                	XmlElement xmlElement_desc = xmlDocument.CreateElement("desc");//创建<name>节点
                	
                	xmlElement_mid.InnerText = dgvActivity.Rows[i].Cells[0].Value.ToString();
               		xmlElement_aid.InnerText = dgvActivity.Rows[i].Cells[1].Value.ToString();
               		xmlElement_aname.InnerText = dgvActivity.Rows[i].Cells[2].Value.ToString();
               		xmlElement_target.InnerText = dgvActivity.Rows[i].Cells[3].Value.ToString();
               		xmlElement_content.InnerText = dgvActivity.Rows[i].Cells[4].Value.ToString();
               		xmlElement_duration.InnerText = dgvActivity.Rows[i].Cells[5].Value.ToString();
               		xmlElement_desc.InnerText = dgvActivity.Rows[i].Cells[6].Value.ToString();
               		
               		xmlElement_activity.AppendChild(xmlElement_mid);
					xmlElement_activity.AppendChild(xmlElement_aid);
					xmlElement_activity.AppendChild(xmlElement_aname);
					xmlElement_activity.AppendChild(xmlElement_target);
					xmlElement_activity.AppendChild(xmlElement_content);
					xmlElement_activity.AppendChild(xmlElement_duration);
					xmlElement_activity.AppendChild(xmlElement_desc);
      
					xmlElement_cmeta.AppendChild(xmlElement_activity);
                    }
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", ""));//编写文件头
                    xmlDocument.AppendChild(xmlElement_cmeta);//将这个<class>附到总文件头，而且设置为根结点
                    xmlDocument.Save(saveFileDialog1.FileName);//保存这个xml文件
                }
                else
                {
                    MessageBox.Show("请保存为XML文件");
                }
            }
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void BtnTopicLoadClick(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();//一个打开文件的对话框
            openFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了文件  
            {
                xml_FilePath_2 = openFileDialog1.FileName;//记录用户选择的文件路径
                XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
                xmlDocument.Load(xml_FilePath_2);//载入路径这个xml
                try
                {
                    XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("cmeta").ChildNodes;//选择class为根结点并得到旗下所有子节点
                    dgvTopic.Rows.Clear();//清空dataGridView1，防止和上次处理的数据混乱
                    foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点
                    {
                        XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<student>
                        //旗下的子节点<name>和<number>分别放入dataGridView1
                        int index = dgvTopic.Rows.Add();//在dataGridView1新加一行，并拿到改行的行标
                        dgvTopic.Rows[index].Cells[0].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加
                        dgvTopic.Rows[index].Cells[1].Value = xmlElement.ChildNodes.Item(1).InnerText;
                    }
                }
                catch
                {
                    MessageBox.Show("XML格式不对！");
                }
            }
            else
            {
                MessageBox.Show("请打开XML文件");
            }
		}
		void BtnTopicWriteClick(object sender, EventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”
            if (xml_FilePath_2 != "")//如果用户已读入xml文件，我们的任务就是修改这个xml文件了
            {
                xmlDocument.Load(xml_FilePath_2);
                XmlNode xmlElement_cmeta = xmlDocument.SelectSingleNode("cmeta");//找到<cmeta>作为根节点
                xmlElement_cmeta.RemoveAll();//删除旗下所有节点
                int row = dgvTopic.Rows.Count;//得到总行数    
                int cell = dgvTopic.Rows[1].Cells.Count;//得到总列数    
                for (int i = 0; i < row - 1; i++)//遍历这个dataGridView
                {
                    XmlElement xmlElement_topic = xmlDocument.CreateElement("topic");
                        
                    XmlElement xmlElement_tid = xmlDocument.CreateElement("tid");//创建一个<mid>节点cmet
                	XmlElement xmlElement_name = xmlDocument.CreateElement("name");//创建<name>节点
                	
                	xmlElement_tid.InnerText = dgvTopic.Rows[i].Cells[0].Value.ToString();
               		xmlElement_name.InnerText = dgvTopic.Rows[i].Cells[1].Value.ToString();
               		
               		xmlElement_topic.AppendChild(xmlElement_tid);
					xmlElement_topic.AppendChild(xmlElement_name);
					
					xmlElement_cmeta.AppendChild(xmlElement_topic);
                }
                xmlDocument.Save(xml_FilePath_2);//保存这个xml
            }
            else//如果用户未读入xml文件，我们的任务就新建一个xml文件了
            {
                SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();//打开一个保存对话框
                saveFileDialog1.Filter = "xml文件(*.xml)|*.xml";//设置允许打开的扩展名
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了一个文件路径
                {
                    XmlElement xmlElement_cmeta = xmlDocument.CreateElement("cmeta");//创建一个<class>节点
                    int row = dgvTopic.Rows.Count;//得到总行数    
                    //int cell = dataGridView1.Rows[1].Cells.Count;//得到总列数    
                    for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
                    {
                        XmlElement xmlElement_topic = xmlDocument.CreateElement("topic");
                        
	                    XmlElement xmlElement_tid = xmlDocument.CreateElement("tid");//创建一个<mid>节点cmet
	                	XmlElement xmlElement_name = xmlDocument.CreateElement("name");//创建<name>节点
	                	
	                	xmlElement_tid.InnerText = dgvTopic.Rows[i].Cells[0].Value.ToString();
	               		xmlElement_name.InnerText = dgvTopic.Rows[i].Cells[1].Value.ToString();
	               		
	               		xmlElement_topic.AppendChild(xmlElement_tid);
						xmlElement_topic.AppendChild(xmlElement_name);
						
						xmlElement_cmeta.AppendChild(xmlElement_topic);
                    }
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", ""));//编写文件头
                    xmlDocument.AppendChild(xmlElement_cmeta);//将这个<class>附到总文件头，而且设置为根结点
                    xmlDocument.Save(saveFileDialog1.FileName);//保存这个xml文件
                    
                }
                else
                {
                    MessageBox.Show("请保存为XML文件");
                }
            }
		}
		void BtnGenDatjsClick(object sender, EventArgs e)
		{
			var catjson = "";
			int row = dgvCatagory.Rows.Count;//得到总行数    
            for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
            {
            	catjson +="{\"mid\" :\"" + dgvCatagory.Rows[i].Cells[0].Value.ToString()+  "\", \"name\": \""+ dgvCatagory.Rows[i].Cells[1].Value.ToString()+"\"},";        		
            }
            catjson = "'["+catjson.Substring(0, catjson.Length -1) + "]'";
            
            
            var activityjson = "";
			row = dgvActivity.Rows.Count;//得到总行数    
            for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
            {
            	activityjson +="{\"mid\" : \""+dgvActivity.Rows[i].Cells[0].Value.ToString()+"\", \"cid\":\""+dgvActivity.Rows[i].Cells[1].Value.ToString()+"\", \"name\" : \""+dgvActivity.Rows[i].Cells[2].Value.ToString()+"\",\"target\":\""+dgvActivity.Rows[i].Cells[3].Value.ToString()+"\",\"duration\":\""+dgvActivity.Rows[i].Cells[4].Value.ToString()+"\", \"content\":\""+dgvActivity.Rows[i].Cells[5].Value.ToString()+"\",\"type\":\""+dgvActivity.Rows[i].Cells[0].Value.ToString()+"\"},";
            }
            activityjson = "'["+activityjson.Substring(0, activityjson.Length -1) + "]'";
            
           	var topicjson = "";
			row = dgvTopic.Rows.Count;//得到总行数    
            for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环    
            {
            	topicjson +="{\"tid\" :\"" + dgvTopic.Rows[i].Cells[0].Value.ToString()+  "\", \"name\": \""+ dgvTopic.Rows[i].Cells[1].Value.ToString()+"\"},";        		
            }
            topicjson = "'["+topicjson.Substring(0, topicjson.Length -1) + "]'";
        
            
            var template = @"var DATA = (function() {
	var category = {0};
	var courses = {1};
	var topic = {2};
	
	return {
		courses: courses,
		category: category,
		topic: topic
	};
})();

";
            var json = template.Replace("{0}", catjson).Replace("{1}", activityjson).Replace("{2}", topicjson);
            System.IO.File.WriteAllText(this.tbxJsonDatOutputPath.Text, json);
            MessageBox.Show("Dat.js 已经生成。");
            
		}
	}
}
