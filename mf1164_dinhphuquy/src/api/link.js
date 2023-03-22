import axios from 'axios'

const service = axios.create({
    baseURL: 'https://localhost:7026/api/v1/',
    timeout: 5000,
    headers: {
    }
})

service.interceptors.request.use(
    config => {
        return config
    },
    error => {
        console.log('error', error)
    }
)

service.interceptors.response.use(
    response => {
        const res = response?.data
        return res
    },
    error => {
        return Promise.reject(error)
    }
)


/**
 * Hàm lấy toàn bộ dữ liệu danh hiệu
 * **/
export function loadDataFull() {
    return service({
        url: 'Emulation',
        method: 'get'
    })
}


/**
 * Hàm load danh sách danh hiệu
 * theo số trang,số bản ghi,tìm kiếm
 * author:DPQuy (13/11/2022)
 * **/
export function loadData(pageIndex, pageSize, filter, filterSuite) {
    return service({
        url: `Emulation/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}&filterSuite=${filterSuite}`,
        method: 'get'
    })
}
/**
 * Lấy danh sách các cấp khen thưởng
 * author:DPQuy (13/11/2022)
 * **/
export function getCommendationLevel() {
    return service({
        url: 'CommendationLevel',
        method: 'get'
    });
}

/**
 * Thêm mới danh hiệu
 * author:DPQuy (13/11/2022)
 * **/
export function insertEmulation(data) {
    return service({
        url: 'Emulation',
        method: 'post',
        data: data
    })
}


/**
 * api cập nhật danh hiệu
 * author:DPQuy (13/11/2022)
 * **/
export function updateEmulation(data) {
    return service({
        url: 'Emulation',
        method: 'put',
        data: data
    })
}

/**
 * Hàm lấy mã danh hiệu mới
 * author:DPQuy (13/11/2022)
 * **/
export async function getNewEmulationCode() {
    return service({
        url: 'Emulation/NewEmulationCode',
        method: 'get'
    })
}

/**
 * api xóa danh hiệu
 * author:DPQuy (13/11/2022)
 * **/
export function deleteEmulation(id) {
    return service({
        url: `Emulation?id=${id}`,
        method: 'delete'
    })
}


/**
 * api xóa nhiều danh hiệu
 * author:DPQuy(13/11/2022)
 * **/
export function deleteMultiple(listID) {
    return service({
        url: `Emulation/deleteMultiple?listID=${listID}`,
        method: 'delete',
    })
}



/**
 * api cập nhật trạng thái đang sử dụng cho 1/nhiều danh hiệu
 * author:DPQuy (13/11/2022)
 * **/
export function updateActiveStatus(listID) {
    return service({
        url: `Emulation/UpdateActiveStatus?listID=${listID}`,
        method: 'post',
    })
}

/**
 * api cập nhật trạng thái ngưng sử dụng cho 1/nhiều danh hiệu
 * author:DPQuy (13/11/2022)
 * **/
export function updateInActiveStatus(listID) {
    return service({
        url: `Emulation/UpdateInActiveStatus?listID=${listID}`,
        method: 'post',
    })
}