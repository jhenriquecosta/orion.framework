"use strict";
const XT_ASSEMBLY = 'Orion.Framework.Web.Blazor.Components';
const XT_SUCCESS_METHOD = 'PromiseCallback';
const XT_ERROR_METHOD = 'PromiseError';

if (!window.xtapp)
{
    window.xtapp = {};
}

window.xtapp = {

    //FUNCTION EXECUTE PROMISSES
    runFunction: (callbackId, fnName, data) =>
    {

        var exec = fnName + '()';
        if (data !== undefined) exec = fnName + "('"+ data + "')";
        var promise = eval(exec);
        //let promise = null;


        //if (data === undefined)
        //{
        //   promise = window[fnName]();
        //}
        //else
        //{
        //   promise = window(fnName)(data);
        //}
        
        console.log(promise);
        promise.then(value => {
            if (value === undefined) {
                value = null;
            }
            const result = JSON.stringify(value);
            DotNet.invokeMethodAsync(XT_ASSEMBLY, XT_SUCCESS_METHOD, callbackId, result);
        }).catch(reason => {
            if (!reason) {
                reason = "Something went wrong";
            }
            const result = reason.toString();
            DotNet.invokeMethodAsync(XT_ASSEMBLY, XT_ERROR_METHOD, callbackId, result);
        });

        // Your function currently has to return something.
        return true;
    },
    //confirm by js
    ConfirmJs: (message) =>
    {
       
        return new Promise((resolve, reject) =>
        {
            var sortir = confirm(message);
            resolve(sortir);
        });
    },
    //sweet Dialog
    SweetDialog: (msg,tipo) =>
    {
        
            var alert = "Info!";
            if (tipo === undefined) tipo = "info";
            if (tipo === "warning") alert = "Atencao!";
            if (tipo === "error") alert = "Erro!";
            if (tipo === "success") alert = "Ok!";
            if (tipo === "info") alert = "Info!";
            swal(alert, msg, tipo);
    },
    //confirm by sweet
     SweetConfirm:  (msg) =>
     { 
         return new Promise((resolve, reject) =>
         {
           resolve
           (
               
               swal({
                 title: 'CONFIRMA ?',
                 text: msg,
                 type: 'warning',
                 showCancelButton: true,
                 confirmButtonColor: '#0CC27E',
                 cancelButtonColor: '#FF586B',
                 confirmButtonText: 'Sim',
                 cancelButtonText: "Nao"
               }).then((isConfirm) =>
               {
                 if (isConfirm)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
               }).catch(swal.noop));
         });
    },
    //tests js
    myPromiseFn: () =>
    {
        
        return new Promise((resolve, reject) =>
        {
            setTimeout(() => {
                resolve("From JS!");
            }, 1000);
        });
    },
    AlertJs: function (title)
    {
      alert(title);
    }, 
    SetFocus: (id) =>
    {
        document.getElementById(id).focus();
    }
    //SetOnBeforeUnload: function ()
    //{
    //    window.onbeforeunload = function () {
    //        return 'Are you sure you want to leave?';
    //    };
    //},
    //UnSetOnBeforeUnload: function ()
    //{
    //    window.onbeforeunload = null;
    //},
    //RaiseEvent: function (el)
    //{   
    //  el = document.getElementById(id);
    //  ev = document.createEvent('Event');
    //  ev.initEvent('change', true, false);
    //  el.dispatchEvent(ev);
    //},
    //showPrompt: function (text)
    //{
    //    return prompt(text, 'Type your name here');
    //},  
    //xtAlertSuccess: function (title, msg)
    //{
    //    swal(title, msg, "success");
    //},
    //xtGetValue: function (element)
    //{  
    //   return element.value;
    //},
  

    //xtAlert: function (title)
    //{
    //    alert(title);
    //} 

    //activateDatePicker: (elementId, formatSubmit) => {
    //    const element = $(`#${elementId}`);

    //    element.datepicker({
    //        uiLibrary: 'bootstrap4',
    //        format: 'yyyy-mm-dd',
    //        showOnFocus: true,
    //        showRightIcon: true,
    //        select: function (e, type) {
    //            // trigger onchange event on the DateEdit component
    //            mutateDOMChange(elementId);
    //        }
    //    });
    //    return true;
    //}
};