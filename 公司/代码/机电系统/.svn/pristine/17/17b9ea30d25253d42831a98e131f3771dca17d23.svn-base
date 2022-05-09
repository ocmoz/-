<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logistics.aspx.cs" Inherits="Module_FM2E_Report_Output_Logistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>报表管理</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
		<link href="../themes/StyleSheet.css" rel="stylesheet"
			type="text/css" />
		<script language="javascript" type="text/javascript">
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
						后勤月报
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_watereletrioil_o.raq','')"
								value="水电柴油" />
						</li>
						
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroommanager_o.raq','')"
								value="食堂管理" />
						</li>
						
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroom_o.raq','')"
								value="食堂开支" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroomenergy_o.raq','')"
								value="食堂能耗" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroomenergy_o.raq','')"
								value="车辆管理" />
						</li>
						<br />
						<br />
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_securitymanager_o.raq','')"
								value="安全管理" />
						</li>
						<tr></tr>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_stationbudgetaction_all_o.raq','')"
								value="预算管理" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_staitonbudgetaction_o.raq','')"
								value="预算附表" />
						</li>
												<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_staitonbudgetaction_sum_o.raq','')"
								value="预算统计" />
						</li>
						<br />
						<br />
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_watereletrioil_p2_o.raq','')"
								value="水（折线图）" />
						</li>
						<br />
						<br />
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_watereletrioil_p1_o.raq','')"
								value="电（折线图）" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_watereletrioil_p3_o.raq','')"
								value="柴油（折线图）" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroommanager_p1_o.raq','')"
								value="食堂人均开支" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroommanager_p2_o.raq','')"
								value="液化气人均消耗" />
						</li>
						<li>
							<input type="button" style="font-family: 微软雅黑;height: 29px; text-align: center; width: 100px;cursor: hand;"
								onclick="javascript:jump('Houqin_diningroommanager_p3_o.raq','')"
								value="柴油人均消耗" />
						</li>
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>

