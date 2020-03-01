function LoadProcessList(pcode) {
    $.ajax({
        type: "post",
        url: "/Ashx/ProcessHandler.ashx",
        data: {
            ProcessId: pcode,
            Action: "GetExprocessList"
        },
        dataType: "json",
        success: function (json) {
            $("#tb_exprocesslist").bootstrapTable('load', json);
        }
    });

}
function LoadExProcessSteps(pcode) {
    $.ajax({
        type: "post",
        url: "/Ashx/ProcessHandler.ashx",
        data: {
            ExProcessId: pcode,
            Action: "GetExProcessSteps"
        },
        dataType: "json",
        success: function (json) {
            $("#tb_exprocesssteplist").bootstrapTable('load', json);
        }
    });
}
function LoadExProcessStepPars(stepid) {
    $.ajax({
        type: "post",
        url: "/Ashx/ProcessHandler.ashx",
        data: {
            StepId: stepid,
            Action: "GetExProcessStepPars"
        },
        dataType: "json",
        success: function (json) {
            $("#tb_exprocessstepparlist").bootstrapTable('load', json);
        }
    });
}


var ExTableInit = function (pcode) {
    var oTableInit = new Object();

    var oStepTable = new setpTableInit("");
    oStepTable.Init();

    //初始化Table
    oTableInit.Init = function () {
        $('#tb_exprocesslist').bootstrapTable({
            url: '/Ashx/ProcessHandler.ashx',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行

            uniqueId: "ExProcessId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选
            columns: [{
                checkbox: true
            }, {
                field: 'ExProcessId',
                title: '子流程编码'
            }, {
                field: 'ExProcessTitle',
                title: '子流程标题'
            }, {
                field: 'StartDeviceName',
                title: '启动流程设备'
            }, {
                field: 'ParTypeString',
                title: '启动流程参数'
            }, {
                field: 'ParValue',
                title: '启动参数'
            }, {
                field: 'ProcessOrderId',
                title: '流程执行顺序'
            }

            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);
                var exprocessId = row.ExProcessId;
                LoadExProcessSteps(exprocessId);

                //$('.info').removeClass('info');//移除class
                //$($element).addClass('info');//添加class
            }
        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            ProcessId: pcode,
            Action: "GetExprocessList"
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


var setpTableInit = function (pcode) {
    var oTableInit = new Object();

    var oParTable = new parTableInit("");
    oParTable.Init();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_exprocesssteplist').bootstrapTable({
            url: '/Ashx/ProcessHandler.ashx',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#steptoolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 350,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "StepId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选

            columns: [{
                checkbox: true
            }, {
                field: 'StepId',
                title: '步骤编号'
            }, {
                field: 'StepTitle',
                title: '步骤标题'
            }, {
                field: 'OrderIndex',
                title: '步骤执行顺序'
            }, {
                field: 'ProcessDeviceName',
                title: '工艺设备编号'
            }, {
                field: 'ActionTypeString',
                title: '操作类型'
            }, {
                field: 'IsSync',
                title: '下一步同步启动',
                formatter: function (value, row, index) {
                    if (value) {
                        return "同步";
                    } else {
                        return "不同步";
                    }
                }
            }, {
                field: 'SyncStepInterval',
                title: '同步间隔(min)'
            }, {
                field: 'FinishParTypeString',
                title: '完成参数类型'
            },
            {
                field: 'FinishParValue',
                title: '完成参数值'
            },
            {
                field: 'FinishParUnit',
                title: '完成参数单位'
            },
            {
                field: 'BeforeStepId',
                title: '上一步编号'
            },
            {
                field: 'Delayed',
                title: '延时量(min)'
            }
            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);

                var stepid = row.StepId;
                LoadExProcessStepPars(stepid);
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            ExProcessId: pcode,
            Action: "GetExProcessSteps"
        };
        return temp;
    };
    return oTableInit;
};

var parTableInit = function (stepid) {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_exprocessstepparlist').bootstrapTable({
            url: '/Ashx/ProcessHandler.ashx',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#partoolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: false,                  //是否显示所有的列(列选择器)
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: undefined,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "ParId",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            singleSelect: true,                    //是否单选

            columns: [{
                checkbox: true
            }, {
                field: 'StepId',
                title: '步骤编号'
            }, {
                field: 'SensorId',
                title: '传感器编号'
            }, {
                field: 'SensorName',
                title: '传感器名称'
            }, {
                field: 'ActionTypeString',
                title: '操作类型'
            }, {
                field: 'ParTypeString',
                title: '参数类型'
            }, {
                field: 'ParValue',
                title: '参数值'
            }, {
                field: 'ParUnit',
                title: '参数单位'
            }, {
                field: 'ParTime',
                title: '运行时间(min)'
            }, {
                field: 'IsFinish',
                title: '是否结束步骤',
                formatter: function (value, row, index) {
                    if (value) {
                        return "结束";
                    } else {
                        return "未结束";
                    }
                }
            }, {
                field: 'LeadTime',
                title: '提前关闭量'
            }
            ],
            onClickRow: function (row, $element) {
                //$('.info').removeClass('info');
                //$($element).addClass('info');
                //alert(row);
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            StepId: stepid,
            Action: "GetExProcessStepPars"
        };
        return temp;
    };
    return oTableInit;
};
