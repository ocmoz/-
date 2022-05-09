<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business.aspx.cs" Inherits="Module_FM2E_Report_Output_Business" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript" charset="gbk">
		function jump(raq,station){
		    var url = "http://";
		    url += "<%=quieeIP%>";
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
						经营环境业务线月报
					</div>
				</div>
			</div>
			<div id="main-tablist">
				<div id="button-bar">
				<ul>
				<!--
					<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_enviroment_o.raq','')"
								value="经营环境月报" align="middle" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_balanceinformation_o.raq','')"
								value="部门管理人员编制信息月报" />
						</li>
						-->
						
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_stationinformation_o.raq','')"
								value="收费站管理人员编制信息月报" />
						</li>
						<!--
						<br />
						<br />
					
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_basicinformation_o.raq','')"
								value="营运部人员基本信息表" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_basicinformation_station_o.raq','')"
								value="收费站人员基本信息表" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;"
								onclick="javascript:jump('Jingying_balancebudgetaction_o.raq','')"
								value="部门预算执行情况月报" />
						</li>
						-->
					</ul>
				</div>
			</div>
		</div>
		
	</body>
</html>
