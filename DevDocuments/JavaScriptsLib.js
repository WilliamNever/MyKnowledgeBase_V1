/*
	Usages -
	var helper = CreateValidation();
if (helper.ValidatingValues) {
    //<span name="CompanyIdValidation" class="error-txt hideblock"><br />* Required</span>
    <style type="text/css">
        .error - txt {
            color: red;
        }
        .hideblock{
            display: none;
        }
        .not-active {
            pointer-events: none;
            cursor: inherit;
        }
    </style>
    let rsl = helper.ValidatingValues([
        { "Key": "CompanyIdValidation", "Value": 'cmpId', "Type": "required", "DefaultValue": '0', "ErrMsg": "Company is required." },
        { "Key": "DivisionIdValidation", "Value": 'divsId', "Type": "required", "DefaultValue": '0', "ErrMsg": "Division is required." },
        { "Key": "FromAddressIdValidation", "Value": 'addrId', "Type": "required", "DefaultValue": '0', "ErrMsg": "From Shipping Address is required." },
        { "Key": "ClientIdValidation", "Value": 'clientId', "Type": "required", "ErrMsg": "B2C Authorization is required." },
    ]);
    if (rsl) {
        helper.ShowModalErrorsList(rsl);
        return;
    }
}
 */

var CreateValidation = function ()
{
	return {
		ShowModalErrors: function (errMsg) {
            InforModal.ShowError({ Title: "Error - ", Messages: errMsg });
        }
        , ShowModalErrorsList: function (stringsArray) {
            this.ShowModalErrors(stringsArray.join("<br/>"));
        }
        , ValidatingValues: function (valistions) {
            let isValided = [];
            $(".error-txt").addClass("hideblock");
            for (var i = 0; i < valistions.length; i++) {
                let isvalid = true;
                let vld = valistions[i];

                if (!vld["Type"] || vld["Type"] === "required") {
                    if (vld["DefaultValue"]) {
                        if (!vld["Value"] || vld["Value"] == vld["DefaultValue"])
                            isvalid = false;
                    }
                    else {
                        if (!vld["Value"]) isvalid = false;
                    }
                }

                if (!isvalid) {
                    isValided.push(vld["ErrMsg"]);
                    $(".error-txt[name='" + vld["Key"] + "']").removeClass("hideblock");
                }
            }
            return isValided.length == 0 ? null : isValided;
        }
	};
};

function GetElementsInPannel(pnlAttr, eleName, attr) {
    //$('[pnl="FrmAddr@Funcs"] button[Func="FrmAddr@AddOrEdit"]');
    return $('[pnl="' + pnlAttr + '"] ' + eleName + '[' + attr + ']');
}
function ReadFormToJson(form) {
    let json = {};
    let vals = $(form).serializeArray();
    vals.forEach(function (val) {
        if (val['value']) {
            if (json[val['name']])
                json[val['name']] = json[val['name']] + ',' + val['value'];
            else
                json[val['name']] = val['value'];
        }
    });
    $(form).find("input:checkbox").each(function () {
        json[this.name] = $(this).is(":checked");
    });
    return json;
}


/*
    Usages - 
    // the followed code must run in the async methods
                let promise = new Promise((s, r) => {
                SendAjaxUrl(
                    {
                        url: this.APIs["SaveUrl"],
                        type: 'Post',
                        data: JSON.stringify(json),
                        dataType: "json",
                        contentType: 'application/json;charset=utf-8',
                    }, { resolve: s, reject: r });
                });

                let rsl = await promise;

 */
function SendAjaxUrl(model, PromiseFuncs) {
    var isSync = model.IsAsync === false;
    if (isSync) {
        if (!$('body').hasClass("not-active")) {
            $('body').addClass("not-active");
        }
        else {
            if (PromiseFuncs?.reject) {
                var info = { xhr: xhr, txtstatus: 'Dropped', err: "For Not Async, this action was dropped.", model: model };
                PromiseFuncs.reject(info);
            }
            console.log("for Not Async, this action was dropped.");
            return;
        }
    }

    if (model.BeforeRunningHandler) model.BeforeRunningHandler();

    $.ajax({
        url: model.url,
        cache: false,
        type: model.type,
        data: model.data,
        dataType: model.dataType,
        contentType: model.contentType,
        async: true,
        processData: model.processData === false ? false : true,
        timeout: 60000,
        success: function (result) {
            if (PromiseFuncs?.resolve)
                PromiseFuncs.resolve(result);
        },
        error: function (xhr, textStatus, errorThrown) {
            /*
                xhr:{
                    status  -   status code
                    readyState  -   current state: 0:not init; 1:loading; 2:loaded; 3:interacting; 4:finished
                    statusText  - error message
                    responseText    -   error message in details
                }
                textStatus - request status
                errorThrown -   not sure
            */
            var info = { xhr: xhr, txtstatus: textStatus, err: errorThrown, model: model };
            console.log(info);

            if (PromiseFuncs?.reject)
                PromiseFuncs.reject(info);
        },
        complete: function (XHR, TS) {
            if (isSync) {
                if ($('body').hasClass("not-active")) {
                    $('body').removeClass("not-active");
                }
            }
            if (PromiseFuncs?.complete)
                PromiseFuncs.complete({ Action: "complete", Messages: { XHR, TS }, model: model });
        }
    });
}



/*
    Usages - 

    htlm definition in pages
    
    <!-- Modal -->
    <div class="modal fade" id="InforModal" data-bs-backdrop="static" style="display:none !important"
        data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" style="max-width:60%;width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="staticBackdropLabel">Modal title</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div>Loading ...</div>
                </div>
                <div class="modal-footer">
                    @*<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Understood</button>*@
                </div>
            </div>
        </div>
    </div>

    js calling, it must run in async methods

    var InforModal = await GetInforModal();
            InforModal.Init({ backdrop: true });
            InforModal.SetTitle("Comany");
            InforModal.SetModalStyle("");
            InforModal.Show();

            InforModal.SetBody(htmlStrings);

            await InforModal.Close();

            InforModal.ShowSuccess({ Title: "", Messages: "Saved successfully!" });
            InforModal.ShowError({ Title: "Error - ", Messages: exp.xhr?.responseJSON?.error });

            the bootstap Modal depends on the followed lib
            Ps - there are some differences in different bootstrap versions. 
                Please adjust the code according to the bootstrap in project.
            Referrences -
                En - https://getbootstrap.com/docs/5.3/components/modal/
                Cn - https://www.bootstrap.cn/doc/read/53.html

            <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
            <script src="~/lib/jquery/dist/jquery.min.js"></script>
            <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
 */

var GetInforModal = async function () {
    return {
        ModalWindow: undefined,
        SetTitle: function (html) {
            $("#InforModal .modal-title").html(html);
        },
        SetBody: function (html) {
            $("#InforModal .modal-body").html(html);
        },
        SetModalStyle: function (style) {
            $("#InforModal .modal-dialog").attr("style", style);
        },
        Show: function () {
            if (!this.ModalWindow) this.Init();
            this.ModalWindow.show();
        },
        Close: async function () {
            if (this.ModalWindow?._isShown === true) {
                let promise = new Promise((s, r) => {
                    $("#InforModal")[0].addEventListener('hidden.bs.modal', event => {
                        s(true);
                    }, { once: true });
                });

                this.ModalWindow?.hide();
                return await promise;
            }
            return true;
        },
        Init: function (options) {
            this.SetTitle("------");
            this.SetBody("Loading ...");
            this.SetModalStyle("max-width:60%;width:60%");
            let opt = options ? options : { backdrop: 'static', keyboard: false };
            this.ModalWindow = new bootstrap.Modal('#InforModal', opt);
        },
        ShowError: function (err) {
            let errModal = new bootstrap.Modal('#InforModal', { backdrop: 'static', keyboard: false });
            this.SetModalStyle("");
            this.SetTitle("<span style='color:red'>" + err?.Title + "</span>");
            this.SetBody("<span style='color:red'>" + err?.Messages + "</span>");
            errModal.show();
        },
        ShowSuccess: function (sucess) {
            let errModal = new bootstrap.Modal('#InforModal', { backdrop: true, keyboard: true });
            this.SetModalStyle("");
            this.SetTitle("<span style='color:green'>" + sucess?.Title + "</span>");
            this.SetBody("<span style='color:green'>" + sucess?.Messages + "</span>");
            errModal.show();
        },
    };
};

/*
Get random strings with specificated char number.
 */
function RandomString(charNum, baseStrings = null) {
    let loop = Math.ceil(charNum) < 1 ? 1 : Math.ceil(charNum);
    let stringBase = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    if (baseStrings)
        stringBase = baseStrings;
    let rsl = '';
    let min = 0;
    let max = stringBase.length;
    for (let i = 0; i < loop; i++) {
        let rnd = Math.floor(Math.random() * (max - min));
        rsl = rsl + stringBase[min + rnd];
    }
    return rsl;
}
