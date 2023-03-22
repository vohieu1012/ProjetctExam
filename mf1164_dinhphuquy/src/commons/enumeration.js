import i18nEnum from "@/i18ns/i18nEnum"
export default {
    /**
     * Cấp khen thưởng
     * DPQuy (13/11/2022)
     * **/
    ComendationLevel: {
        VillageLevel: 4, //Cấp Xã/tương đương
        DistrictLevel: 3, //Cấp Huyện/tương đương
        ProvinceLevel: 2, //Cấp Tỉnh/tương đương
        GovernmentLevel: 1, //Cấp nhà nước
    },

    /**
     * đối tượng khen thưởng
     * 
     * **/
    ApplyObject: {
        Individual: 1, //cá nhân
        Team: 2, //tập thể
        Both: 3 //cá nhân và tập thể
    },

    /**
     * enum cho sinh mã code tự động
     * **/
    GenerateCode: {
        allow: 1, //cho phép sinh mã code theo tên
        notallow: 0,//không cho phép
    },
    /**
     * Phong trào áp dụng
     * 
     * **/
    MovementType: {
        Batched: 1, //theo đợt
        Frequent: 2, //thường xuyên
        Both: 3 //thường xuyên,theo đợt
    },

    /**
     * enum message cảnh báo
     * **/
    Message: {
        MessageMultiple: 3,  //xóa nhiều
        MessageSystem: 2,  //Cảnh báo dữ liệu hệ thống
        MessageSingle: 1, //xóa một
        DuplicateCode: 0  //trùng mã
    },


    /**
     * trạng thái danh hiệu
     * **/
    Status: {
        Active: 1,  //sử dụng
        Inactive: 0,  //ngưng sử dụng
    },


    /**
     * enum dữ liệu hệ thống
     * **/
    IsSystem: {
        isSystem: 1, //là dữ liệu hệ thống
        noSystem: 0, //không phải dữ liệu hệ thống
    },



    /***
     * apply object dùng cho bộ lọc
     * **/
    ApplyObjectFilter: [
        {
            Text: "Tất cả",
            Value: 0,
        },
        {
            Text: i18nEnum.ApplyObject.Individual,
            Value: 1
        },
        {
            Text: i18nEnum.ApplyObject.Team,
            Value: 2
        },
        {
            Text: i18nEnum.ApplyObject.Both,
            Value: 3
        }
    ],



    /**
     * Cấp khen thưởng dùng cho bộ lọc
     * **/
    CommendationLevelFilter: [
        {
            Text: "Tất cả",
            Value: 0,
        },
        {
            Text: i18nEnum.CommendationLevel.GovernmentLevel,
            Value: "142cb08f-7c31-21fa-8e90-67245e8b283e",
        },
        {
            Text: i18nEnum.CommendationLevel.ProvinceLevel,
            Value: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
        },
        {
            Text: i18nEnum.CommendationLevel.DistrictLevel,
            Value: "17120d02-6ab5-3e43-18cb-66948daf6128",
        },
        {
            Text: i18nEnum.CommendationLevel.VillageLevel,
            Value: "469b3ece-744a-45d5-957d-e8c757976496",
        },
    ],


    /**
     * Loại phong trào dùng cho bộ lọc
     * **/
    MovementTypeFilter: [
        {
            Text: "Tất cả",
            Value: 3,
        },
        {
            Text: i18nEnum.MovementType.Batched,
            Value: 1,
        },
        {
            Text: i18nEnum.MovementType.Frequent,
            Value: 2,
        },

    ],


    /**
     * Trạng thái dùng cho bộ lọc
     * **/
    StatusFilter: [
        {
            Text: "Tất cả",
            Value: 2,
        },
        {
            Text: i18nEnum.Status.Active,
            Value: 1
        },
        {
            Text: i18nEnum.Status.Inactive,
            Value: 0,
        }

    ],

    /**
     * enum cho form
     * **/
    FormMode: {
        AddMode: 0, //thêm mới
        EditMode: 1, //sửa
    },

    /**
     * enum lỗi
     * **/
    ErrorCode: {
        BadRequest: 0,
        ServerError: 1,
    }

}