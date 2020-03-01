﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HNFactoryAutoSystem.Web.Dome.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Scripts/bootstrap-3.3.7-dist/css/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-3.3.7-dist/js/bootstrap.js"></script>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>

    <script src="../Scripts/bootstrap-table/bootstrap-table.js"></script>
    <link href="../Scripts/bootstrap-table/bootstrap-table.min.css" rel="stylesheet" />
    <script>
        $(function () {

            //1.初始化Table
            var oTable = new TableInit();
            oTable.Init();

            //2.初始化Button的点击事件
            var oButtonInit = new ButtonInit();
            oButtonInit.Init();

        });


        var TableInit = function () {
            var oTableInit = new Object();
            //初始化Table
            oTableInit.Init = function () {
                $('#tb_departments').bootstrapTable({
                    url: '/Home/GetDepartment',         //请求后台的URL（*）
                    method: 'get',                      //请求方式（*）
                    toolbar: '#toolbar',                //工具按钮用哪个容器
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: true,                   //是否显示分页（*）
                    sortable: false,                     //是否启用排序
                    sortOrder: "asc",                   //排序方式
                    queryParams: oTableInit.queryParams,//传递参数（*）
                    sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                    pageNumber: 1,                       //初始化加载第一页，默认第一页
                    pageSize: 10,                       //每页的记录行数（*）
                    pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                    search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
                    strictSearch: false,
                    showColumns: false,                  //是否显示所有的列(列选择框)
                    showRefresh: false,                  //是否显示刷新按钮
                    minimumCountColumns: 2,             //最少允许的列数
                    clickToSelect: true,                //是否启用点击选中行
                    height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                    uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
                    showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
                    cardView: false,                    //是否显示详细视图
                    detailView: false,                   //是否显示父子表
                    columns: [{
                        checkbox: true
                    }, {
                        field: 'Name',
                        title: '部门名称'
                    }, {
                        field: 'ParentName',
                        title: '上级部门'
                    }, {
                        field: 'Level',
                        title: '部门级别'
                    }, {
                        field: 'Desc',
                        title: '描述'
                    },]
                });
            };

            //得到查询的参数
            oTableInit.queryParams = function (params) {
                var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    limit: params.limit,   //页面大小
                    offset: params.offset,  //页码
                    departmentname: $("#txt_search_departmentname").val(),
                    statu: $("#txt_search_statu").val()
                };
                return temp;
            };
            return oTableInit;
        };


        var ButtonInit = function () {
            var oInit = new Object();
            var postdata = {};

            oInit.Init = function () {
                //初始化页面上面的按钮事件
            };

            return oInit;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel-body" style="padding-bottom: 0px;">
            <div class="panel panel-default">
                <div class="panel-heading">查询条件</div>
                <div class="panel-body">
                    <form id="formSearch" class="form-horizontal">
                        <div class="form-group" style="margin-top: 15px">
                            <label class="control-label col-sm-1" for="txt_search_departmentname">部门名称</label>
                            <div class="col-sm-3">
                                <input type="text" class="form-control" id="txt_search_departmentname">
                            </div>
                            <label class="control-label col-sm-1" for="txt_search_statu">状态</label>
                            <div class="col-sm-3">
                                <input type="text" class="form-control" id="txt_search_statu">
                            </div>
                            <div class="col-sm-4" style="text-align: left;">
                                <button type="button" style="margin-left: 50px" id="btn_query" class="btn btn-primary">查询</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>

            <div id="toolbar" class="btn-group">
                <button id="btn_add" type="button" class="btn btn-default">
                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>新增
                </button>
                <button id="btn_edit" type="button" class="btn btn-default">
                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>修改
                </button>
                <button id="btn_delete" type="button" class="btn btn-default">
                    <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>删除
                </button>
            </div>
            <table id="tb_departments"></table>
        </div>
    </form>
</body>
</html>
