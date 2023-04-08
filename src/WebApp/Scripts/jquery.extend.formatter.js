//-------账号类型---------//
var accounttypefiltersource = [{ value: '', text: 'All'}];
var accounttypedatasource = [];
accounttypefiltersource.push({ value: '企业',text:'企业'  });
accounttypedatasource.push({ value: '企业',text:'企业'  });
accounttypefiltersource.push({ value: '供应商',text:'供应商'  });
accounttypedatasource.push({ value: '供应商',text:'供应商'  });
//for datagrid AccountType field  formatter
function accounttypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = accounttypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = accounttypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   AccountType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
accounttypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: accounttypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   AccountType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
accounttypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: accounttypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------投标状态---------//
var bstatusfiltersource = [{ value: '', text: 'All'}];
var bstatusdatasource = [];
bstatusfiltersource.push({ value: '待开标',text:'待开标'  });
bstatusdatasource.push({ value: '待开标',text:'待开标'  });
bstatusfiltersource.push({ value: '中标',text:'中标'  });
bstatusdatasource.push({ value: '中标',text:'中标'  });
bstatusfiltersource.push({ value: '出局',text:'出局'  });
bstatusdatasource.push({ value: '出局',text:'出局'  });
bstatusfiltersource.push({ value: '已确认',text:'已确认'  });
bstatusdatasource.push({ value: '已确认',text:'已确认'  });
bstatusfiltersource.push({ value: '发货中',text:'发货中'  });
bstatusdatasource.push({ value: '发货中',text:'发货中'  });
bstatusfiltersource.push({ value: '结案中',text:'结案中'  });
bstatusdatasource.push({ value: '结案中',text:'结案中'  });
bstatusfiltersource.push({ value: '作废',text:'作废'  });
bstatusdatasource.push({ value: '作废',text:'作废'  });
bstatusfiltersource.push({ value: '废标',text:'废标'  });
bstatusdatasource.push({ value: '废标',text:'废标'  });
bstatusfiltersource.push({ value: '已结案',text:'已结案'  });
bstatusdatasource.push({ value: '已结案',text:'已结案'  });
//for datagrid bstatus field  formatter
function bstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = bstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = bstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   bstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
bstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: bstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   bstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
bstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: bstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------确认状态筛选---------//
var bstatus1filtersource = [{ value: '', text: 'All'}];
var bstatus1datasource = [];
bstatus1filtersource.push({ value: '中标',text:'中标'  });
bstatus1datasource.push({ value: '中标',text:'中标'  });
bstatus1filtersource.push({ value: '废标',text:'废标'  });
bstatus1datasource.push({ value: '废标',text:'废标'  });
//for datagrid bstatus1 field  formatter
function bstatus1formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = bstatus1datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = bstatus1datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   bstatus1  field filter 
$.extend($.fn.datagrid.defaults.filters, {
bstatus1filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: bstatus1filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   bstatus1   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
bstatus1editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: bstatus1datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------发货状态筛选---------//
var bstatus2filtersource = [{ value: '', text: 'All'}];
var bstatus2datasource = [];
bstatus2filtersource.push({ value: '已确认',text:'已确认'  });
bstatus2datasource.push({ value: '已确认',text:'已确认'  });
//for datagrid bstatus2 field  formatter
function bstatus2formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = bstatus2datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = bstatus2datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   bstatus2  field filter 
$.extend($.fn.datagrid.defaults.filters, {
bstatus2filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: bstatus2filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   bstatus2   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
bstatus2editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: bstatus2datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------结案状态查询---------//
var closestatusfiltersource = [{ value: '', text: 'All'}];
var closestatusdatasource = [];
closestatusfiltersource.push({ value: '结案中',text:'结案中'  });
closestatusdatasource.push({ value: '结案中',text:'结案中'  });
closestatusfiltersource.push({ value: '已结案',text:'已结案'  });
closestatusdatasource.push({ value: '已结案',text:'已结案'  });
//for datagrid closestatus field  formatter
function closestatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = closestatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = closestatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   closestatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
closestatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: closestatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   closestatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
closestatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: closestatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------文件类型---------//
var filetypefiltersource = [{ value: '', text: 'All'}];
var filetypedatasource = [];
filetypefiltersource.push({ value: '0',text:'txt'  });
filetypedatasource.push({ value: '0',text:'txt'  });
filetypefiltersource.push({ value: '1',text:'xls'  });
filetypedatasource.push({ value: '1',text:'xls'  });
filetypefiltersource.push({ value: '2',text:'pdf'  });
filetypedatasource.push({ value: '2',text:'pdf'  });
filetypefiltersource.push({ value: '3',text:'xlsx'  });
filetypedatasource.push({ value: '3',text:'xlsx'  });
filetypefiltersource.push({ value: '4',text:'json'  });
filetypedatasource.push({ value: '4',text:'json'  });
filetypefiltersource.push({ value: '5',text:'js'  });
filetypedatasource.push({ value: '5',text:'js'  });
filetypefiltersource.push({ value: '6',text:'html'  });
filetypedatasource.push({ value: '6',text:'html'  });
filetypefiltersource.push({ value: '7',text:'xml'  });
filetypedatasource.push({ value: '7',text:'xml'  });
filetypefiltersource.push({ value: '8',text:'cs'  });
filetypedatasource.push({ value: '8',text:'cs'  });
filetypefiltersource.push({ value: '9',text:'doc'  });
filetypedatasource.push({ value: '9',text:'doc'  });
filetypefiltersource.push({ value: '10',text:'docx'  });
filetypedatasource.push({ value: '10',text:'docx'  });
filetypefiltersource.push({ value: '11',text:'py'  });
filetypedatasource.push({ value: '11',text:'py'  });
filetypefiltersource.push({ value: '12',text:'c'  });
filetypedatasource.push({ value: '12',text:'c'  });
filetypefiltersource.push({ value: '13',text:'java'  });
filetypedatasource.push({ value: '13',text:'java'  });
//for datagrid FileType field  formatter
function filetypeformatter(value, row, index) { 
     let multiple = true; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = filetypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = filetypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   FileType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
filetypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: filetypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   FileType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
filetypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: filetypedatasource,
         multiple: true,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------禁用标志---------//
var isdisabledfiltersource = [{ value: '', text: 'All'}];
var isdisableddatasource = [];
isdisabledfiltersource.push({ value: '0',text:'未禁用'  });
isdisableddatasource.push({ value: '0',text:'未禁用'  });
isdisabledfiltersource.push({ value: '1',text:'已禁用'  });
isdisableddatasource.push({ value: '1',text:'已禁用'  });
//for datagrid IsDisabled field  formatter
function isdisabledformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isdisableddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isdisableddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsDisabled  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isdisabledfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isdisabledfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsDisabled   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isdisablededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isdisableddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------启用标志---------//
var isenabledfiltersource = [{ value: '', text: 'All'}];
var isenableddatasource = [];
isenabledfiltersource.push({ value: '0',text:'未启用'  });
isenableddatasource.push({ value: '0',text:'未启用'  });
isenabledfiltersource.push({ value: '1',text:'已启用'  });
isenableddatasource.push({ value: '1',text:'已启用'  });
//for datagrid IsEnabled field  formatter
function isenabledformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isenableddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isenableddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsEnabled  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isenabledfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isenabledfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsEnabled   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isenablededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isenableddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------已读标志---------//
var isnewfiltersource = [{ value: '', text: 'All'}];
var isnewdatasource = [];
isnewfiltersource.push({ value: '0',text:'未读'  });
isnewdatasource.push({ value: '0',text:'未读'  });
isnewfiltersource.push({ value: '1',text:'已读'  });
isnewdatasource.push({ value: '1',text:'已读'  });
//for datagrid IsNew field  formatter
function isnewformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isnewdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isnewdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsNew  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isnewfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isnewfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsNew   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isneweditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isnewdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------通知标志---------//
var isnoticefiltersource = [{ value: '', text: 'All'}];
var isnoticedatasource = [];
isnoticefiltersource.push({ value: '0',text:'未发'  });
isnoticedatasource.push({ value: '0',text:'未发'  });
isnoticefiltersource.push({ value: '1',text:'已发'  });
isnoticedatasource.push({ value: '1',text:'已发'  });
//for datagrid IsNotice field  formatter
function isnoticeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isnoticedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isnoticedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsNotice  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isnoticefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isnoticefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsNotice   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isnoticeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isnoticedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------NLogLevel---------//
var levelfiltersource = [{ value: '', text: 'All'}];
var leveldatasource = [];
levelfiltersource.push({ value: 'Error',text:'Error'  });
leveldatasource.push({ value: 'Error',text:'Error'  });
levelfiltersource.push({ value: 'Info',text:'Info'  });
leveldatasource.push({ value: 'Info',text:'Info'  });
levelfiltersource.push({ value: 'Debug',text:'Debug'  });
leveldatasource.push({ value: 'Debug',text:'Debug'  });
levelfiltersource.push({ value: 'Trace',text:'Trace'  });
leveldatasource.push({ value: 'Trace',text:'Trace'  });
levelfiltersource.push({ value: 'Warn',text:'Warn'  });
leveldatasource.push({ value: 'Warn',text:'Warn'  });
levelfiltersource.push({ value: 'Fatal',text:'Fatal'  });
leveldatasource.push({ value: 'Fatal',text:'Fatal'  });
//for datagrid Level field  formatter
function levelformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = leveldatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = leveldatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Level  field filter 
$.extend($.fn.datagrid.defaults.filters, {
levelfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: levelfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Level   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
leveleditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: leveldatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------二级库名---------//
var locfiltersource = [{ value: '', text: 'All'}];
var locdatasource = [];
locfiltersource.push({ value: 'b01',text:'606试剂室'  });
locdatasource.push({ value: 'b01',text:'606试剂室'  });
locfiltersource.push({ value: 'b02',text:'207试剂室'  });
locdatasource.push({ value: 'b02',text:'207试剂室'  });
//for datagrid loc field  formatter
function locformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = locdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = locdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   loc  field filter 
$.extend($.fn.datagrid.defaults.filters, {
locfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: locfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   loc   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
loceditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: locdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------日志分组---------//
var messagegroupfiltersource = [{ value: '', text: 'All'}];
var messagegroupdatasource = [];
messagegroupfiltersource.push({ value: '0',text:'系统操作'  });
messagegroupdatasource.push({ value: '0',text:'系统操作'  });
messagegroupfiltersource.push({ value: '1',text:'业务操作'  });
messagegroupdatasource.push({ value: '1',text:'业务操作'  });
messagegroupfiltersource.push({ value: '2',text:'接口操作'  });
messagegroupdatasource.push({ value: '2',text:'接口操作'  });
//for datagrid MessageGroup field  formatter
function messagegroupformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = messagegroupdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = messagegroupdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   MessageGroup  field filter 
$.extend($.fn.datagrid.defaults.filters, {
messagegroupfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: messagegroupfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   MessageGroup   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
messagegroupeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: messagegroupdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------日志类型---------//
var messagetypefiltersource = [{ value: '', text: 'All'}];
var messagetypedatasource = [];
messagetypefiltersource.push({ value: '0',text:'Information'  });
messagetypedatasource.push({ value: '0',text:'Information'  });
messagetypefiltersource.push({ value: '1',text:'Error'  });
messagetypedatasource.push({ value: '1',text:'Error'  });
messagetypefiltersource.push({ value: '2',text:'Alert'  });
messagetypedatasource.push({ value: '2',text:'Alert'  });
//for datagrid MessageType field  formatter
function messagetypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = messagetypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = messagetypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   MessageType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
messagetypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: messagetypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   MessageType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
messagetypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: messagetypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------通知分组---------//
var notifygroupfiltersource = [{ value: '', text: 'All'}];
var notifygroupdatasource = [];
notifygroupfiltersource.push({ value: '0',text:'系统推送'  });
notifygroupdatasource.push({ value: '0',text:'系统推送'  });
notifygroupfiltersource.push({ value: '1',text:'业务推送'  });
notifygroupdatasource.push({ value: '1',text:'业务推送'  });
notifygroupfiltersource.push({ value: '2',text:'接口推送'  });
notifygroupdatasource.push({ value: '2',text:'接口推送'  });
notifygroupfiltersource.push({ value: '3',text:'手工推送'  });
notifygroupdatasource.push({ value: '3',text:'手工推送'  });
//for datagrid NotifyGroup field  formatter
function notifygroupformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = notifygroupdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = notifygroupdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   NotifyGroup  field filter 
$.extend($.fn.datagrid.defaults.filters, {
notifygroupfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: notifygroupfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   NotifyGroup   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
notifygroupeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: notifygroupdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------优先级---------//
var priorityfiltersource = [{ value: '', text: 'All'}];
var prioritydatasource = [];
priorityfiltersource.push({ value: '1',text:'高'  });
prioritydatasource.push({ value: '1',text:'高'  });
priorityfiltersource.push({ value: '2',text:'中'  });
prioritydatasource.push({ value: '2',text:'中'  });
priorityfiltersource.push({ value: '3',text:'低'  });
prioritydatasource.push({ value: '3',text:'低'  });
priorityfiltersource.push({ value: '4',text:'低级'  });
prioritydatasource.push({ value: '4',text:'低级'  });
//for datagrid Priority field  formatter
function priorityformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = prioritydatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = prioritydatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Priority  field filter 
$.extend($.fn.datagrid.defaults.filters, {
priorityfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: priorityfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Priority   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
priorityeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: prioritydatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------收货人联系方式---------//
var receiverfiltersource = [{ value: '', text: 'All'}];
var receiverdatasource = [];
receiverfiltersource.push({ value: '王亮香',text:'13921309955'  });
receiverdatasource.push({ value: '王亮香',text:'13921309955'  });
//for datagrid receiver field  formatter
function receiverformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = receiverdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = receiverdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   receiver  field filter 
$.extend($.fn.datagrid.defaults.filters, {
receiverfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: receiverfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   receiver   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
receivereditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: receiverdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------NLogResolved---------//
var resolvedfiltersource = [{ value: '', text: 'All'}];
var resolveddatasource = [];
resolvedfiltersource.push({ value: 'false',text:'未处理'  });
resolveddatasource.push({ value: 'false',text:'未处理'  });
resolvedfiltersource.push({ value: 'true',text:'已处理'  });
resolveddatasource.push({ value: 'true',text:'已处理'  });
//for datagrid Resolved field  formatter
function resolvedformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = resolveddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = resolveddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Resolved  field filter 
$.extend($.fn.datagrid.defaults.filters, {
resolvedfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: resolvedfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Resolved   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
resolvededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: resolveddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------发货单状态---------//
var shipstatusfiltersource = [{ value: '', text: 'All'}];
var shipstatusdatasource = [];
shipstatusfiltersource.push({ value: '发货中',text:'发货中'  });
shipstatusdatasource.push({ value: '发货中',text:'发货中'  });
shipstatusfiltersource.push({ value: '结案中',text:'结案中'  });
shipstatusdatasource.push({ value: '结案中',text:'结案中'  });
shipstatusfiltersource.push({ value: '已结案',text:'已结案'  });
shipstatusdatasource.push({ value: '已结案',text:'已结案'  });
//for datagrid shipstatus field  formatter
function shipstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = shipstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = shipstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   shipstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
shipstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: shipstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   shipstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
shipstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: shipstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------是否允许发送短信---------//
var smsfiltersource = [{ value: '', text: 'All'}];
var smsdatasource = [];
smsfiltersource.push({ value: '0',text:'不发'  });
smsdatasource.push({ value: '0',text:'不发'  });
//for datagrid sms field  formatter
function smsformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = smsdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = smsdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   sms  field filter 
$.extend($.fn.datagrid.defaults.filters, {
smsfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: smsfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   sms   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
smseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: smsdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------Test---------//
var statusfiltersource = [{ value: '', text: 'All'}];
var statusdatasource = [];
statusfiltersource.push({ value: '待处理',text:'待处理'  });
statusdatasource.push({ value: '待处理',text:'待处理'  });
statusfiltersource.push({ value: '发标中',text:'发标中'  });
statusdatasource.push({ value: '发标中',text:'发标中'  });
statusfiltersource.push({ value: '开标中',text:'开标中'  });
statusdatasource.push({ value: '开标中',text:'开标中'  });
statusfiltersource.push({ value: '发货中',text:'发货中'  });
statusdatasource.push({ value: '发货中',text:'发货中'  });
statusfiltersource.push({ value: '结案中',text:'结案中'  });
statusdatasource.push({ value: '结案中',text:'结案中'  });
statusfiltersource.push({ value: '已结案',text:'已结案'  });
statusdatasource.push({ value: '已结案',text:'已结案'  });
statusfiltersource.push({ value: '废标待确认',text:'废标待确认'  });
statusdatasource.push({ value: '废标待确认',text:'废标待确认'  });
statusfiltersource.push({ value: '废标已确认',text:'废标已确认'  });
statusdatasource.push({ value: '废标已确认',text:'废标已确认'  });
statusfiltersource.push({ value: '待发货',text:'待发货'  });
statusdatasource.push({ value: '待发货',text:'待发货'  });
statusfiltersource.push({ value: '已发货',text:'已发货'  });
statusdatasource.push({ value: '已发货',text:'已发货'  });
//for datagrid Status field  formatter
function statusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = statusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = statusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Status  field filter 
$.extend($.fn.datagrid.defaults.filters, {
statusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: statusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Status   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
statuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: statusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------发标状态筛选---------//
var status1filtersource = [{ value: '', text: 'All'}];
var status1datasource = [];
status1filtersource.push({ value: '待处理',text:'待处理'  });
status1datasource.push({ value: '待处理',text:'待处理'  });
status1filtersource.push({ value: '发标中',text:'发标中'  });
status1datasource.push({ value: '发标中',text:'发标中'  });
//for datagrid status1 field  formatter
function status1formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status1datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status1datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status1  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status1filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status1filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status1   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status1editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status1datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------投标状态筛选---------//
var status2filtersource = [{ value: '', text: 'All'}];
var status2datasource = [];
status2filtersource.push({ value: '发标中',text:'发标中'  });
status2datasource.push({ value: '发标中',text:'发标中'  });
status2filtersource.push({ value: '开标中',text:'开标中'  });
status2datasource.push({ value: '开标中',text:'开标中'  });
//for datagrid status2 field  formatter
function status2formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status2datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status2datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status2  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status2filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status2filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status2   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status2editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status2datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------开标状态筛选---------//
var status3filtersource = [{ value: '', text: 'All'}];
var status3datasource = [];
status3filtersource.push({ value: '开标中',text:'开标中'  });
status3datasource.push({ value: '开标中',text:'开标中'  });
status3filtersource.push({ value: '中标待确认',text:'中标待确认'  });
status3datasource.push({ value: '中标待确认',text:'中标待确认'  });
status3filtersource.push({ value: '废标待确认',text:'废标待确认'  });
status3datasource.push({ value: '废标待确认',text:'废标待确认'  });
//for datagrid status3 field  formatter
function status3formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status3datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status3datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status3  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status3filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status3filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status3   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status3editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status3datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------确认状态筛选---------//
var status4filtersource = [{ value: '', text: 'All'}];
var status4datasource = [];
status4filtersource.push({ value: '确认中',text:'确认中'  });
status4datasource.push({ value: '确认中',text:'确认中'  });
status4filtersource.push({ value: '发货中',text:'发货中'  });
status4datasource.push({ value: '发货中',text:'发货中'  });
//for datagrid status4 field  formatter
function status4formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status4datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status4datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status4  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status4filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status4filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status4   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status4editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status4datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------收货状态筛选---------//
var status5filtersource = [{ value: '', text: 'All'}];
var status5datasource = [];
status5filtersource.push({ value: '已发货',text:'已发货'  });
status5datasource.push({ value: '已发货',text:'已发货'  });
//for datagrid status5 field  formatter
function status5formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status5datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status5datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status5  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status5filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status5filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status5   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status5editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status5datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------结案状态筛选---------//
var status6filtersource = [{ value: '', text: 'All'}];
var status6datasource = [];
status6filtersource.push({ value: '结案中',text:'结案中'  });
status6datasource.push({ value: '结案中',text:'结案中'  });
status6filtersource.push({ value: '已结案',text:'已结案'  });
status6datasource.push({ value: '已结案',text:'已结案'  });
//for datagrid status6 field  formatter
function status6formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status6datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status6datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status6  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status6filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status6filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status6   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status6editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status6datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------投标记录状态---------//
var status7filtersource = [{ value: '', text: 'All'}];
var status7datasource = [];
status7filtersource.push({ value: '投标中',text:'投标中'  });
status7datasource.push({ value: '投标中',text:'投标中'  });
status7filtersource.push({ value: '开标中',text:'开标中'  });
status7datasource.push({ value: '开标中',text:'开标中'  });
status7filtersource.push({ value: '废标',text:'废标'  });
status7datasource.push({ value: '废标',text:'废标'  });
//for datagrid status7 field  formatter
function status7formatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = status7datasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = status7datasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   status7  field filter 
$.extend($.fn.datagrid.defaults.filters, {
status7filter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: status7filtersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   status7   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
status7editor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: status7datasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------单位代码---------//
var unitfiltersource = [{ value: '', text: 'All'}];
var unitdatasource = [];
unitfiltersource.push({ value: '035',text:'千克'  });
unitdatasource.push({ value: '035',text:'千克'  });
unitfiltersource.push({ value: '007',text:'个'  });
unitdatasource.push({ value: '007',text:'个'  });
unitfiltersource.push({ value: '001',text:'台'  });
unitdatasource.push({ value: '001',text:'台'  });
unitfiltersource.push({ value: '002',text:'座'  });
unitdatasource.push({ value: '002',text:'座'  });
unitfiltersource.push({ value: '003',text:'辆'  });
unitdatasource.push({ value: '003',text:'辆'  });
unitfiltersource.push({ value: '004',text:'艘'  });
unitdatasource.push({ value: '004',text:'艘'  });
unitfiltersource.push({ value: '005',text:'架'  });
unitdatasource.push({ value: '005',text:'架'  });
unitfiltersource.push({ value: '006',text:'套'  });
unitdatasource.push({ value: '006',text:'套'  });
//for datagrid unit field  formatter
function unitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = unitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = unitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   unit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
unitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: unitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   unit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
uniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: unitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------任务状态---------//
var workstatusfiltersource = [{ value: '', text: 'All'}];
var workstatusdatasource = [];
workstatusfiltersource.push({ value: '0',text:'等待指派'  });
workstatusdatasource.push({ value: '0',text:'等待指派'  });
workstatusfiltersource.push({ value: '1',text:'未开始'  });
workstatusdatasource.push({ value: '1',text:'未开始'  });
workstatusfiltersource.push({ value: '2',text:'进行中'  });
workstatusdatasource.push({ value: '2',text:'进行中'  });
workstatusfiltersource.push({ value: '3',text:'已完成'  });
workstatusdatasource.push({ value: '3',text:'已完成'  });
workstatusfiltersource.push({ value: '4',text:'关闭'  });
workstatusdatasource.push({ value: '4',text:'关闭'  });
//for datagrid workstatus field  formatter
function workstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = workstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = workstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   workstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
workstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: workstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   workstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
workstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: workstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
