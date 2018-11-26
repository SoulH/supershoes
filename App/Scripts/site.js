var isCompleteMenu = true;

function apiUrlFor(path) {
    return $("body").attr("api_url") + path;
}

function collapseMenu() {
    if (isCompleteMenu) {
        $(".title").css("display", "none");
        $(".menu").css("display", "none");
        $(".left a").css("display", "none");
        $("#img-home").css("display", "block");
        $("#img-about").css("display", "block");
        $("#link-home").css("display", "block");
        $("#link-about").css("display", "block");
        $(".grid").css("grid-template-columns", "80px auto");
    } else {
        $(".grid").css("grid-template-columns", "80px 120px auto");
        $(".title").css("display", "grid");
        $(".menu").css("display", "inline-flex");
        $(".left a").css("display", "block");
        $("#img-home").css("display", "none");
        $("#img-about").css("display", "none");
        $("#link-home").css("display", "none");
        $("#link-about").css("display", "none");

    }
    isCompleteMenu = !isCompleteMenu;
}

function showUserMenu() {
    let dis = $(".user-menu").css("display");
    if (dis === "none")
        $(".user-menu").fadeIn("slow");
    else
        $(".user-menu").fadeOut("slow");
    $("#admin-name").css("text-decoration-line", "none");
    $("#admin-name").css("color", "white");
}

function cleanTokenStorage() {
    localStorage.removeItem("token");
}

function login() {
    let url = apiUrlFor("/account/authenticate");
    let user = {};
    user.username = $("#Username").val();
    user.password = $("#Password").val();

    $.post(url, user).done((response) => {
        console.log("athenticate request success");
        console.log(response);
        if (response.success) {
            localStorage.setItem("token", response.data[0].token);
            $("#Token").val(response.data[0].token);
            $("#login-form").submit();
        }
    });
}

// Use example: await checkAuth()
function checkAuth() {
    let token = localStorage.getItem("token");
    return new Promise((resolve, reject) => {
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            url: apiUrlFor("/account/auth"),
            success: function (response) {
                resolve(response);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                let err = {};
                err.success = false;
                err.error_code = XMLHttpRequest.status;
                err.error_message = XMLHttpRequest.error_message;
                console.log(err);
                alert("You will be log out because your service security token has expired.\n\nYou will be must log in again.");
                location.href = $("#log-out").attr("href") || "/account/logout";
            },
            type: 'GET',
            contentType: 'json'
        });
    });
}

// Use example: await apiRequest("get","/account/auth")
// Parameters:
//  * method : "get" || "post" || "update" || "delete"
//  * path: "/account/auth"
//  * obj: { "name" : "something" }
function apiRequest(method, path, obj) {
    let token = localStorage.getItem("token");
    console.log(method);
    console.log(path);
    console.log(JSON.stringify(obj));
    console.log(token);
    return new Promise((resolve, reject) => {
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            url: apiUrlFor(path),
            data: JSON.stringify(obj),
            success: function (response) {
                resolve(response);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                let err = {};
                err.success = false;
                err.error_code = XMLHttpRequest.status;
                err.error_message = XMLHttpRequest.error_message;
                console.log("apirequestError");
                console.log(err);
                resolve(err);
            },
            type: method,
            contentType: 'json',
            dataType: 'json'
        });
    });
}

function getArticle() {
    let model = {};
    model.id = $("#Id").val() || 0;
    model.name = $("#Name").val();
    model.description = $("#Description").val();
    model.price = $("#Price").val();
    model.total_in_shelf = $("#TotalInShelf").val();
    model.total_in_vault = $("#TotalInVault").val();
    model.store_id = $("#StoreId").val();
    return model;
}

function getStore() {
    let model = {};
    model.id = $("#Id").val() || 0;
    model.name = $("#Name").val();
    model.address = $("#Address").val();
    return model;
}

function articleSearch() {
    let pos = location.href.indexOf("?");
    let href = (pos < 0) ? location.href : location.href.substring(0, pos);
    href = href.replace("/search", "");
    let name = !$("#search-in").val() ? "" : `&name=${$("#search-in").val()}`;
    let storeId = $("#store-id-sel").val();
    if (storeId === "0")
        location.href = `${href}?take=50&${name}`;
    else
        location.href = `${href}/search?take=50&storeid=${storeId}${name}`;
}

async function createArticle() {
    console.log("createArticle");
    let x = await checkAuth();
    if (!x.success) return;
    console.log("createArticle : authenticated user");
    let model = getArticle();
    console.log(model);
    let res = await apiRequest("post", "/articles", model);
    if (res.success)
        location.href = $("#back-lnk").attr("href");
    else alert(res.error_message || "500 - Server Error");
}

async function updateArticle() {
    let x = await checkAuth();
    if (!x.success) return;
    
    let model = getArticle();
    let res = await apiRequest("put", `/articles/${model.id}`, model);
    if (res.success)
        location.href = $("#back-lnk").attr("href");
    else alert(res.error_message || "500- Server Error");
}

async function deleteArticle(id) {
    let x = await checkAuth();
    if (!x.success) return;

    let res = await apiRequest("delete", `/articles/${id}`);
    if (res.success)
        articleSearch();
    else alert(res.error_message || "500 -Server Error");
}

function storeSearch() {
    let pos = location.href.indexOf("?");
    let href = (pos < 0) ? location.href : location.href.substring(0, pos);
    let name = !$("#search-in").val() ? "" : `&name=${$("#search-in").val()}`;
    location.href = `${href}?take=50${name}`;
}

async function createStore() {
    console.log("createStore");
    let x = await checkAuth();
    if (!x.success) return;
    console.log("createStore : authenticated user");
    
    let model = getStore();
    console.log(model);
    console.log(apiUrlFor("/stores"));
    let res = await apiRequest("post", "/stores", model);
    if (res.success)
        location.href = $("#back-lnk").attr("href");
    else alert(res.error_message);
}

async function updateStore() {
    let x = await checkAuth();
    if (!x.success) return;

    let model = getStore();
    let res = await apiRequest("put", `/stores/${model.id}`, model);
    if (res.success)
        location.href = $("#back-lnk").attr("href");
    else alert(res.error_message || "500 - Server Error");
}

async function deleteStore(id) {
    let x = await checkAuth();
    if (!x.success) return;
    
    let res = await apiRequest("delete", `/stores/${id}`);
    if (res.success)
        storeSearch();
    else alert(res.error_message || "500 - Server Error");
}


$(document).ready(function () {
    $("#menu-toggle").click(() => collapseMenu());
    $("#admin-name").click(() => showUserMenu());
    $("#log-out").click(() => cleanTokenStorage());
});