///<reference path="libs/oidc-client.js" /> 
///<reference path="uiSessionMonitor.js" /> 
 



var config = {
    authority: "http://localhost:63238",  
    client_id: "Development",
    redirect_uri: window.location.origin + "/callback.html",
    post_logout_redirect_uri: window.location.origin + "/index.html",

    // if we choose to use popup window instead for logins
    popup_redirect_uri: window.location.origin + "/popup.html",
    popupWindowFeatures: "menubar=yes,location=yes,toolbar=yes,width=1200,height=800,left=100,top=100;resizable=yes",

    // these two will be done dynamically from the buttons clicked, but are
    // needed if you want to use the silent_renew
    response_type: "id_token token",
    scope: "openid profile FrameworkAPI",//"openid profile email api1 api2.read_only",

    // this will toggle if profile endpoint is used
    loadUserInfo: true,

    // silent renew will get a new access_token via an iframe 
    // just prior to the old access_token expiring (60 seconds prior)
    silent_redirect_uri: window.location.origin + "/silent.html",
    automaticSilentRenew: true,
    
    // will revoke (reference) access tokens at logout time
    revokeAccessTokenOnSignout: true,
    accessTokenExpiringNotificationTime: 60,//用来调整accessTokenExpiring激发时间.默认是过期前60 秒。
    monitorSession: true, //开启监控session
    checkSessionInterval: 2000,// 2秒验证一次session
    // this will allow all the OIDC protocol claims to be visible in the window. normally a client app
    // wouldn't care about them or want them taking up space
    filterProtocolClaims: false 
  
};
//Oidc.Log.logger = window.console;
//Oidc.Log.level = Oidc.Log.DEBUG;

var timer;
var isOpenTimer = false;


var last = new Date().getTime(),
    curr = new Date().getTime(), 
    out = 2 * 60 * 1000; //设置超时时间： 2分
 
var mgr = new Oidc.UserManager(config);

mgr.events.addUserLoaded(function (user) {
    log("User loaded");
    showTokens();
});
mgr.events.addUserUnloaded(function () {
    log("User logged out locally");
    showTokens();
});

 

var uiSessionMonitor = new uiSessionMonitor(5 * 60 * 1000, 2 * 60 * 1000,2000);

uiSessionMonitor.start();

uiSessionMonitor.events.addUISessionExpiring(function () {
    console.log("会话快过期");
});

uiSessionMonitor.events.addUISessionCountdown(function (timeRemaining) { 
    var hour = Math.floor(((timeRemaining / 1000) % 86400) / 3600),
        minutes = Math.floor(((timeRemaining / 1000) % 3600) / 60),
        seconds = Math.floor((timeRemaining / 1000) % 60);
    console.log("您的会话将在" + hour + "时" + minutes + "分" + seconds + "秒后过期");
    display("#ajax-result", "您的会话将在" + hour + "时" + minutes + "分" + seconds + "秒后过期");
});

uiSessionMonitor.events.addUISessionExpired(function () {
    console.log("会话已过期");
});

uiSessionMonitor.events.addRefreshUISession(function () {
    console.log("刷新会话");
});


function uiSessionMonitorStart() {
    uiSessionMonitor.start();
}

function uiSessionMonitorStop() {
    uiSessionMonitor.stop();
}

function uiSessionMonitorRefreshSession() { 
    uiSessionMonitor.refreshUISession();
}  

function countDown(maxtime) {

    if (isOpenTimer)
    {
        return;
    }

    isOpenTimer = true; 
    console.log("打开计时器"); 
      timer = setInterval(function () {
        if (!!maxtime) {
            var hour = Math.floor(((maxtime/1000) % 86400) / 3600 ),
                minutes = Math.floor(((maxtime / 1000) % 3600) / 60 ),
                seconds = Math.floor((maxtime / 1000) % 60 );
                display("#ajax-result","您的会话将在" +  hour + "时" + minutes + "分" + seconds + "秒后过期"); 
            maxtime = maxtime - 1000;
        } else {
            clearInterval(timer);
            alert("会话过期");
            logout();
        }
    }, 1000);
}


//您可以在访问令牌到期事件中手动执行此操作-仅在用户存在的情况下显式执行静默续订。
mgr.events.addAccessTokenExpiring(function () {
    //log("==========================Access token expiring...======================");

    //var currTime = new Date().getTime(); //更新当前时间
    //var templast = localStorage.getItem("UserLastActiveTime"); 
    ////判断templast是否为空

    //if (currTime - templast > out) { //判断是否超时
    //    console.log("addAccessTokenExpiring会话过期：" + templast);
    //    //alert("会话过期");
    //    //logout();
    //    //用户一段时间不操作，就开始会话过期倒计时

    //    //var nowTime = new Date().getTime(); //更新当前时间

    //    //设置倒计时过期时间
    //    //localStorage.setItem("SessionExpiresTime", (nowTime + out)) 
         
    //    //countDown(out);

    //    //alert("会话过期倒计时开始");

    //    //var inter = setInterval(function () {/*定时器  间隔1秒检测是否长时间未操作页面 */
    //    //    curr = new Date().getTime(); //更新当前时间

    //    //    var sessionExpiresTime = localStorage.getItem("SessionExpiresTime");

    //    //    localStorage.setItem("SessionExpiresTime", (sessionExpiresTime - 1000))

    //    //    console.log("定时检查是否未操作页面：" + sessionExpiresTime);

    //    //    display("#ajax-result", "您的会话将在" + (curr - sessionExpiresTime) + "毫秒后过期");
    //    //    //判断templast是否为空 
    //    //    if (curr - sessionExpiresTime > 0) { //判断是否超时
    //    //        clearInterval(inter);//清空定时器
    //    //        console.log("那么长时间没未操作了！");//超时操作
    //    //        alert("会话过期");
    //    //        logout();
    //    //    }
    //    //}, 1000);

    //}
    //else
    //{  
    //    clearInterval(timer);
    //    isOpenTimer = false;
    //    console.log("关闭计时器");
    //    display("#ajax-result", "用户进行操作，会话延期"); 
    //    //alert("会话未过期，刷新token");
    //    console.log("addAccessTokenExpiring会话未过期，刷新token：" + templast); 
    //    mgr.signinSilent()
    //}

});
mgr.events.addSilentRenewError(function (err) {
    log("Silent renew error: " + err.message);
});
mgr.events.addUserSignedOut(function () {
    log("User signed out of OP");
});

function login() {
    mgr.signinRedirect();
}

function popup() {
    mgr.signinPopup().then(function () {
        log("Logged In");
    });
}

function logout() {
    mgr.signoutRedirect();
}

function revoke() {
    mgr.revokeAccessToken().then(function () {
        log("Access Token Revoked.")
    }).catch(function (err) {
        log(err);
    });
}

function renewToken() {
    mgr.signinSilent()
        .then(function () {
            log("silent renew success");
            showTokens();
        }).catch(function (err) {
            log("silent renew error", err);
        });
}
function callApi() {
    mgr.getUser().then(function (user) {
        var xhr = new XMLHttpRequest();
        xhr.onload = function (e) {
            if (xhr.status >= 400)
            {
                //display("#ajax-result", {
                //    status: xhr.status,
                //    statusText: xhr.statusText,
                //    wwwAuthenticate: xhr.getResponseHeader("WWW-Authenticate")
                //});
            }
            else
            {
                DisplayAPIResult(xhr.response);
                //display("#ajax-result", xhr.response);
            }
        };
        //xhr.open("GET", "http://localhost:51179/weatherforecast", true);
        xhr.open("GET", "http://localhost:44387/test", true);
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}






function DisplayAPIResult(data) {
    var dataStr = data;
    if (data && typeof data === 'string') {
        data = JSON.parse(data);
    }
    if (data) {
        data = JSON.stringify(data, null, 2);
    } 
    data = JSON.parse(data); 
    var myDate = new Date();
    var mytime = myDate.toLocaleString();        //获取日期与时间 
    document.getElementById('response').innerHTML += "<br>当前时间：" + mytime + "，获取API结果：" + JSON.stringify(dataStr) + '\r\n';
}

function checkUserExpired() {
    mgr.getUser().then(function (user) {
        if (user.expired) {
            alert("user expired");
        }
        else { 
            alert("user not expired");
        }
    });
}

//定时请求API
setInterval("callApi()", 6000);

if (window.location.hash) {
    handleCallback();
}

document.querySelector(".login").addEventListener("click", login, false);
//document.querySelector(".popup").addEventListener("click", popup, false);
document.querySelector(".renew").addEventListener("click", renewToken, false);
document.querySelector(".call").addEventListener("click", callApi, false);
document.querySelector(".revoke").addEventListener("click", revoke, false);
document.querySelector(".logout").addEventListener("click", logout, false);
document.querySelector(".checkUserExpired").addEventListener("click", checkUserExpired, false);
document.querySelector(".uiSessionMonitorStart").addEventListener("click", uiSessionMonitorStart, false);
document.querySelector(".uiSessionMonitorStop").addEventListener("click", uiSessionMonitorStop, false);
document.querySelector(".uiSessionMonitorRefreshSession").addEventListener("click", uiSessionMonitorRefreshSession, false);



function log(data) {
    document.getElementById('response').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('response').innerHTML += msg + '\r\n';
    });
}

function display(selector, data) {
    if (data && typeof data === 'string') {
        try {
            data = JSON.parse(data);
        }
        catch (e) { }
    }
    if (data && typeof data !== 'string') {
        data = JSON.stringify(data, null, 2);
    }
    document.querySelector(selector).textContent = data;
}

function showTokens() {
    mgr.getUser().then(function (user) {
        if (user) {
            display("#id-token", user);
        }
        else {
            log("Not logged in");
        }
    });
}
showTokens();

function handleCallback() {
    mgr.signinRedirectCallback().then(function (user) {
        var hash = window.location.hash.substr(1);
        var result = hash.split('&').reduce(function (result, item) {
            var parts = item.split('=');
            result[parts[0]] = parts[1];
            return result;
        }, {});

        log(result);
        showTokens();

        window.history.replaceState({},
            window.document.title,
            window.location.origin + window.location.pathname);

    }, function (error) {
        log(error);
    });
}