import axios from 'axios';
import qs from 'qs';
import tools from './tools';
//
axios.defaults.timeout = 100 * 1000;
//axios.defaults.baseURL = 'http://47.99.152.236:8083/';
axios.defaults.baseURL = 'http://localhost:52473/';
let isloading = true;
//http request 拦截器
axios.interceptors.request.use(config => {
    if (isloading) {
        global.tools.loading.start();
    }

    config.headers['x-requested-width'] = 'XMLHttpRequest';
    config.headers['Authorization'] = tools.getAuthorization();
    // config.headers['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    config.headers['Content-Type'] = 'application/json; charset=UTF-8';

    if (!config.data) return config;

    if (config.data.isUpload)
        config.headers['Content-Type'] = 'multipart/form-data';
    //else
    //config.data = qs.stringify(config.data); //如果是非上传类型 则 将数据重新组装

    return config;
}, error => {
    console.log(error);
    return Promise.reject(error);
});

//http response 拦截器
axios.interceptors.response.use(response => {
    global.tools.loading.close();
    var data = response.data;

    if (data.hasOwnProperty('status')) {
        //     程序异常 = -2,
        // 未授权 = -1,
        // 失败 = 0,
        // 成功 = 1,

        if (data.status == -1) { //接口授权码无效
            tools.alert(data.msg + ',请先登录授权!', "警告", () => global.$router.push("/login"));
            return;
        }
        if (data.status == -2) { //服务端异常
            tools.alert(data.msg, "错误");
            return;
        }
        if (data.status == 0) { //失败
            tools.msg(data.msg, '警告');
            return;
        }
    }

    return response;
}, error => {
    console.log(error);
    global.tools.loading.close();
    if (error.response.status === 401) {
        global.tools.notice("请先登录授权!", "错误");
        return global.$router.push('/login');
    } else {
        return Promise.reject(error)
    }
});

/**
 * 封装get方法
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @param headers 头部信息
 * @returns {Promise}
 */
export function get(url, data = null, loading = true, config = {}) {
    isloading = loading;
    if (data) url += `?${qs.stringify(data)}`;
    return new Promise((resolve, reject) => {
        axios.get(url, config)
            .then(response => {
                resolve(response);
            })
            .catch(err => {
                reject(err)
            })
    })
}

/**
 * 封装post请求
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @param config config信息
 * @returns {Promise}
 */
export function post(url, data = null, loading = true, config = {}) {
    isloading = loading;
    return new Promise((resolve, reject) => {
        axios.post(url, data, config)
            .then(response => {
                if (response != undefined) {
                    resolve(response);
                }
            }, err => {
                reject(err)
            })
    })
}

/**
 * 封装 post 请求 用于上传文件 
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @param config config信息
 * @returns {Promise}
 */
export function upload(url, data = null, loading = true, config = {}) {
    isloading = loading;
    if (!data) data = {};
    data.isUpload = true;
    return new Promise((resolve, reject) => {
        axios.post(url, data, config)
            .then(response => {
                if (response != undefined) {
                    resolve(response);
                }
            }, err => {
                reject(err)
            })
    })
}

/**
 * 封装 get请求 用于下载文件
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @returns {Promise}
 */
export function download(url, data = null, loading = true) {
    this.get(url, data, loading, {
        // responseType: 'stream',
        responseType: 'blob',
        // responseType: 'arraybuffer',
    }).then(res => {
        var data = res.data;
        var headers = res.headers;
        //"attachment; filename=6a9c13bc-e214-44e4-8456-dbca9fcd2367.xls;filename*=UTF-8''6a9c13bc-e214-44e4-8456-dbca9fcd2367.xls"
        var contentDisposition = headers['content-disposition'];
        var contentType = headers['content-type'];
        var attachmentInfoArrary = contentDisposition.split(';');
        var fileName = '';
        if (attachmentInfoArrary.length > 1) {
            fileName = attachmentInfoArrary[1].split('=')[1];
        }
        var blob = new Blob([data], { type: contentType });

        if (window.navigator && window.navigator.msSaveOrOpenBlob) { // IE
            window.navigator.msSaveOrOpenBlob(blob, fileName);
        } else {
            let url = (window.URL || window.webkitURL).createObjectURL(blob);
            // window.open(url, "_blank"); //下载
            // window.URL.revokeObjectURL(url) // 只要映射存在，Blob就不能进行垃圾回收，因此一旦不再需要引用，就必须小心撤销URL，释放掉blob对象。

            let a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.setAttribute('download', fileName);
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a); // 下载完成移除元素
            // window.location.href = url
            window.URL.revokeObjectURL(url); // 只要映射存在，Blob就不能进行垃圾回收，因此一旦不再需要引用，就必须小心撤销URL，释放掉blob对象。

        }
    });
}

/**
 * 封装patch请求
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @param config config信息
 * @returns {Promise}
 */
export function patch(url, data = null, loading = true, config = {}) {
    isloading = loading;
    return new Promise((resolve, reject) => {
        axios.patch(url, data, config)
            .then(response => {
                resolve(response);
            }, err => {
                reject(err)
            })
    })
}

/**
 * 封装put请求
 * @param url
 * @param data
 * @param loading 是否有加载效果
 * @param config config信息
 * @returns {Promise}
 */
export function put(url, data = null, loading = true, config = {}) {
    isloading = loading;
    return new Promise((resolve, reject) => {
        axios.put(url, data, config)
            .then(response => {
                resolve(response);
            }, err => {
                reject(err)
            })
    })
}