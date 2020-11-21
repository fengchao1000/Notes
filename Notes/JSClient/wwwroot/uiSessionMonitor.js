
var UserLastActiveTimeKey = "UserLastActiveTimeKey";
var IsRefreshSessionKey = "IsRefreshSessionKey";

function uiSessionMonitor(sessionLifetime, sessionExpiringNotificationTime, interval) {

    this.sessionLifetime = sessionLifetime;
    this.sessionExpiringNotificationTime = sessionExpiringNotificationTime;
    this.interval = interval; 

    this.start = function () { 
        console.log("uiSessionMonitor.start");
        document.onmouseover = function () {
            localStorage.setItem(UserLastActiveTimeKey, new Date().getTime());
            console.log("UserLastActiveTime:" + new Date().getTime()); 
        };
        this.stop();
        this.checkSessionTimer = setInterval(this.checkSession.bind(this), this.interval);
    };

    this.stop = function () {

        if (this.checkSessionTimer) {
            console.log("uiSessionMonitor.stop");

            clearInterval(this.checkSessionTimer);
            this.checkSessionTimer = null;
        }
    };

    this.checkSession = function () {

        console.log("uiSessionMonitor.checkSession");
        var currentTime = new Date().getTime();
        var userLastActiveTime = localStorage.getItem(UserLastActiveTimeKey);
        var expiring = sessionLifetime - (currentTime - userLastActiveTime); 

        if ((currentTime - userLastActiveTime) > (sessionLifetime - sessionExpiringNotificationTime)) {
            console.log("uiSessionMonitor.checkSession: expired timer in:" + expiring);
            localStorage.setItem(IsRefreshSessionKey, false); 
            this.sessionCountdownStart();
            this.stop();
        }
        else {
            console.log("uiSessionMonitor.checkSession: expiring timer in:" + expiring);
        }
    };

    this.sessionCountdownStart = function () {

        console.log("uiSessionMonitor.sessionCountdown");
        var maxtime = this.sessionExpiringNotificationTime;

        this.sessionCountdownTimer = setInterval(function () {

            //如果刷新Session
            var isRefreshSession = localStorage.getItem(IsRefreshSessionKey);

            if (isRefreshSession = "true")
            {
                console.log("uiSessionMonitor.sessionCountdown:isRefreshSession=true");
                clearInterval(this.sessionCountdownTimer);
                this.sessionCountdownTimer = null;
                return;
            }

            if (!!maxtime) {
                var hour = Math.floor(((maxtime / 1000) % 86400) / 3600),
                    minutes = Math.floor(((maxtime / 1000) % 3600) / 60),
                    seconds = Math.floor((maxtime / 1000) % 60);
                display("#ajax-result", "您的会话将在" + hour + "时" + minutes + "分" + seconds + "秒后过期");
                maxtime = maxtime - 1000;
            }
            else {
                clearInterval(this.sessionCountdownTimer);
                alert("会话过期");
                logout();
            }
        }, 1000);
    };

    this.sessionCountdownStop = function () {

        if (this.sessionCountdownTimer) {
            console.log("uiSessionMonitor.sessionCountdownStop");

            clearInterval(this.sessionCountdownTimer);
            this.sessionCountdownTimer = null;
        }
    };

    this.refreshSession = function () {
        console.log("uiSessionMonitor.refreshSession");
        localStorage.setItem(UserLastActiveTimeKey, new Date().getTime());
        localStorage.setItem(IsRefreshSessionKey, true); 
        this.sessionCountdownStop();
        this.start();
    };

}