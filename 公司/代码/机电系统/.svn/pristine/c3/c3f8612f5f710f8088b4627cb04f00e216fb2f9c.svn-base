<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Streatlight.aspx.cs" Inherits="Module_FM2E_Report_Input_Streatlight" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
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

	<body>
		<div id="main">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						路灯隧道所信息月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" id="b1" runat="server"
							    onclick="javascript:jump('Ludeng_LudengSuidao_i.raq','a')"
								value="路灯隧道所信息月报" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 170px;cursor: hand;"/>
						</li>
						<!--
						<li>
							<input type="button" 
								onclick="javascript:jump('Ludeng_EMCShuju_i.raq','')"
								value="EMC数据统计" style="font-family: 微软雅黑;height: 29px; text-align: center;  width: 170px;cursor: hand;"/>
						</li>
						<li>
							<input type="button" 
								onclick="javascript:jump('Ludeng_Suidaojidian_i.raq','')"
								value="设备维修情况统计表" style="font-family: 微软雅黑;height: 29px; text-align: center;  width: 170px;cursor: hand;"/>
						</li>
						-->
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
