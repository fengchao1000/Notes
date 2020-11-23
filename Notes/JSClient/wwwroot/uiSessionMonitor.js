 
var UserLastActiveTimeKey = "UserLastActiveTimeKey";
var IsRefreshSessionKey = "IsRefreshSessionKey";

//uiSessionMonitor UI会话监控， uiSessionLifetime：用户未操作时会话时长；uiSessionExpiringNotificationTime：通知时间，倒计时时间；checkUISessionInterval：定时检查会话时间
function uiSessionMonitor(uiSessionLifetime, uiSessionExpiringNotificationTime, checkUISessionInterval) {

    this.uiSessionLifetime = uiSessionLifetime;
    this.uiSessionExpiringNotificationTime = uiSessionExpiringNotificationTime;
    this.checkUISessionInterval = checkUISessionInterval; 
    this.events = new uiSessionMonitorEvents(); 

    this.start = function () {  
        document.onmouseover = function () {
            localStorage.setItem(UserLastActiveTimeKey, new Date().getTime());
            console.log("UserLastActiveTime:" + new Date().getTime()); 
        };
        localStorage.setItem(IsRefreshSessionKey, true); 
        localStorage.setItem(UserLastActiveTimeKey, new Date().getTime()); 
        this.stop();
        console.log("uiSessionMonitor.start");
        this.checkUISessionTimer = setInterval(this.checkUISession.bind(this), this.checkUISessionInterval);
    };

    this.stop = function () {

        if (this.checkUISessionTimer) {
            console.log("uiSessionMonitor.stop"); 
            clearInterval(this.checkUISessionTimer);
            this.checkUISessionTimer = null;
        }
    };

    this.checkUISession = function () {

        console.log("uiSessionMonitor.checkUISession");
        var currentTime = new Date().getTime();
        var userLastActiveTime = localStorage.getItem(UserLastActiveTimeKey);
        var expiring = uiSessionLifetime - (currentTime - userLastActiveTime); 

        if ((currentTime - userLastActiveTime) > (uiSessionLifetime - uiSessionExpiringNotificationTime)) {
            console.log("uiSessionMonitor.checkUISession: expired timer in:" + expiring);
            localStorage.setItem(IsRefreshSessionKey, false); 
            this.events.raiseUISessionExpiring();  
            this.uiSessionCountdownStart();
            this.stop();
        }
        else {
            console.log("uiSessionMonitor.checkUISession: expiring timer in:" + expiring);
        }
    };

    this.uiSessionCountdownStart = function () {

        console.log("uiSessionMonitor.sessionCountdown");
        var timeRemaining = this.uiSessionExpiringNotificationTime;

        this.uiSessionCountdownTimer = setInterval(function () {

            //如果其他页面刷新Session
            var isRefreshSession = localStorage.getItem(IsRefreshSessionKey);

            if (isRefreshSession == "true")
            {
                console.log("uiSessionMonitor.sessionCountdown:isRefreshSession=true");
                clearInterval(this.uiSessionCountdownTimer); 
                this.refreshUISession();
                return;
            }

            if (!!timeRemaining) { 
                timeRemaining = timeRemaining - 1000;
                this.events.raiseUISessionCountdown(timeRemaining);  
            }
            else {
                clearInterval(this.uiSessionCountdownTimer); 
                this.events.raiseUISessionExpired();
            }
        }.bind(this), 1000);
    };

    this.uiSessionCountdownStop = function () {

        if (this.uiSessionCountdownTimer) {
            console.log("uiSessionMonitor.uiSessionCountdownStop");

            clearInterval(this.uiSessionCountdownTimer);
            this.uiSessionCountdownTimer = null;
        }
    };

    this.refreshUISession = function () {
        console.log("uiSessionMonitor.refreshUISession");
        this.uiSessionCountdownStop(); 
        this.events.raiseRefreshUISession(); 
        this.start();
    }; 
}


//uiSessionMonitor事件
function uiSessionMonitorEvents() {

    this.uiSessionExpiring = new Event("uiSessionExpiring");
    this.uiSessionExpired = new Event("uiSessionExpired");
    this.refreshUISession = new Event("refreshUISession");
    this.uiSessionCountdown = new Event("uiSessionCountdown");

    this.addUISessionExpiring = function (cb) {
        this.uiSessionExpiring.addHandler(cb);
    };

    this.removeUISessionExpiring = function (cb) {
        this.uiSessionExpiring.removeHandler(cb);
    }; 

    this.raiseUISessionExpiring = function (e) {
        console.log("uiSessionMonitorEvents.raiseRefreshUISession");
        this.uiSessionExpiring.raise(e);
    };  

    this.addUISessionExpired = function (cb) {
        this.uiSessionExpired.addHandler(cb);
    };

    this.removeUISessionExpired = function (cb) {
        this.uiSessionExpired.removeHandler(cb);
    };

    this.raiseUISessionExpired = function (e) {
        console.log("uiSessionMonitorEvents.raiseUISessionExpired");
        this.uiSessionExpired.raise(e);
    }; 

    this.addUISessionCountdown = function (cb) {
        this.uiSessionCountdown.addHandler(cb);
    };

    this.removeUISessionCountdown = function (cb) {
        this.uiSessionCountdown.removeHandler(cb);
    };

    this.raiseUISessionCountdown = function (timeRemaining) { 
        console.log("uiSessionMonitorEvents.raiseUISessionCountdown:" + timeRemaining);
        var raiseEvent = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : true;
        if (raiseEvent) {
            this.uiSessionCountdown.raise(timeRemaining);
        }
    };   

    this.addRefreshUISession = function (cb) {
        this.refreshUISession.addHandler(cb);
    };

    this.removeRefreshUISession = function (cb) {
        this.refreshUISession.removeHandler(cb);
    };

    this.raiseRefreshUISession = function (e) { 
        console.log("uiSessionMonitorEvents.raiseRefreshUISession");
        this.refreshUISession.raise(e); 
    };  
}

//事件
function Event(name) { 

    this._name = name;
    this._callbacks = [];

    this.addHandler = function addHandler(cb) {
        this._callbacks.push(cb);
    };

    this.removeHandler = function removeHandler(cb) {
        var idx = this._callbacks.findIndex(function (item) {
            return item === cb;
        });
        if (idx >= 0) {
            this._callbacks.splice(idx, 1);
        }
    };

    this.raise = function raise() {
        console.log("Event: Raising event: " + this._name); 
        for (var i = 0; i < this._callbacks.length; i++) {
            var _callbacks;

            (_callbacks = this._callbacks)[i].apply(_callbacks, arguments);
        }
    }; 
};
 