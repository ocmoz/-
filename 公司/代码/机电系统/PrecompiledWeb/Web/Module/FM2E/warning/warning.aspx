<%@ page language="C#" autoeventwireup="true" inherits="Module_FM2E_warning_warning, App_Web_vfz5laot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="WebUtility" Namespace="WebUtility.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<html  runat="server" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>自动预警</title>
		<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
        <link href="../Report/themes/StyleSheet.css" rel="stylesheet" type="text/css" />
		<script language="javascript" type="text/javascript">
		    function jump(raq, station) {
		        var url = "http://<%=warningIP%>:8094/ElectricalMaintenance"		   
            window.open(url);
		}
        </script>
	</head>

	<body runat="server">
		<div id="main" runat="server">
			<div id="table">
				<div id="ptk">
					<div id="tabtop-l"></div>
					<div id="tabtop-z">
						自动预警
					</div>
				</div>
			</div>
			
			<div id="main-tablist">
				<div id="button-bar">
					<ul>
						<li>
							<input type="button"   id="b1" runat="server"
								onclick="javascript:jump('Jingying_envioment_i_1.raq','a')"
								value="自动预警模块"  style="font-family: 微软雅黑;height: 29px; text-align: center; width: 210px;cursor: hand;" />
						</li>					
					</ul>
				</div>
			</div>
			</div>
	</body>
</html>
