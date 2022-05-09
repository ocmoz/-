<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business.aspx.cs" Inherits="Module_FM2E_Report_Input_Business" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<html   xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript" charset="gbk">
		function jump(raq,station){
		    var url = "http://";
		    var ip1="<%=quieeIP1%>";
		    var ip2="<%=quieeIP2%>";
		    var tempurl =document.location.href;
		    if (tempurl.indexOf(ip2)>0) {
		        url += "<%=quieeIP1%>";
		    }
		    else{
		    	url += "<%=quieeIP%>";	    
		    }
		    url += "/quiee/reportJsp/showReport.jsp"
            if (raq!="") url += "?raq=/" + raq;
            if ("<%=station%>"!="") url += "&station=" + "<%=station%>";
            window.open(url);
		}
        </script>
	</head>

	<body runat="server" >
		<div id="main">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						经营环境业务线月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul runat="server">
					<!--
						<li>
							<input type="button"  
								onclick="javascript:jump('Jingying_envioment_i_1.raq','')"
								value="直管路段-经营环境月报"  style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;" />
						</li>
						
						<li>
							<input type="button"  
								onclick="javascript:jump('Jingying_balanceinformation_i.raq','')"
								value="部门管理人员编制信息月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;" />
						</li>
						-->
						
						<li runat="server" id="li1"  >
							<input  type="button" id="button1" runat="server" 
							 	onclick="jump('Jingying_stationinformation_i.raq','a')"
								value="收费站管理人员编制信息月报" style="font-family: 微软雅黑; height: 29px; text-align: center; width: 210px;cursor: hand;" />
						</li>
						<!--
						<br />
						<br />
						<li>
							<input type="button"  
								onclick="javascript:jump('Jingying_basicinformation_i.raq','')"
								value="营运部人员基本信息表" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"/>
						</li>
						<br />
						<br />
						<li>
							<input type="button"  
								onclick="javascript:jump('Jingying_basicinformation_station_i.raq','')"
								value="收费站人员基本信息表"style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;" />
						</li><li>
							<input type="button"  
								onclick="javascript:jump('Jingying_balancebudgetaction_i.raq','')"
								value="部门预算执行情况月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"/>
						</li>
						-->
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
