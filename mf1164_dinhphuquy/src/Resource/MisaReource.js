import { createI18n } from 'vue-i18n'
import i18nEnum from '@/i18ns/i18nEnum'
import enumeration from '@/commons/enumeration'
const misaResource = createI18n({
    locale: "vn",
    messages: {
        vn: {
            i18nEnum,
            enumeration,
            EditText: 'Sửa',
            EditEmulationText: 'Sửa danh hiệu thi đua',
            AddEmulationText: 'Thêm danh hiệu thi đua',
            AddEmutaion: 'Thêm danh hiệu',
            selectedText: 'Đã chọn',
            uncheckedText: 'Bỏ chọn',
            EmulationNameText: 'Tên danh hiệu thi đua',
            EmulationText: 'Danh hiệu thi đua',
            EmulationNameEmpty: 'Tên danh hiệu thi đua không được để trống',
            EmulationCodeText: 'Mã danh hiệu',
            EmulationCodeDuplicateText: 'Mã danh hiệu đã tồn tại trong hệ thống',
            EmulationCodeEmpty: 'Mã danh hiệu không được để trống',
            MovementTypeEmpty: 'Loại phong trào không được để trống.',
            ApplyObjectText: 'Đối tượng khen thưởng',
            ApplyObjectEmpty: 'Đối tượng khen thưởng không được để trống.',
            CommendationLevelNameText: 'Cấp khen thưởng',
            CommendationLevelNameEmpty: 'Cấp khen thưởng không được để trống',
            MovementTypeTextApply: 'Loại phong trào áp dụng',
            MovementTypeText: 'Loại phong trào',
            NoteText: 'Ghi chú',
            DeleteMessageSingle: 'Xóa danh hiệu thi đua',
            MessageSytemDelete: 'Danh hiệu Chiến sĩ thi đua cơ sở là dữ liệu hệ thống, bạn không thể xóa',
            warningChange: 'Dữ liệu đã thay đổi, bạn có chắc chắn muốn đóng form không?',
            NameProject: 'MISA CeGov',
            FilterText: 'Bộ lọc',
            StatusText: 'Trạng thái',
            CloseText: 'Đóng',
            HelpText: 'Hướng dẫn',
            CancelText: 'Hủy',
            SaveSucces: 'Lưu thành công',
            DeleteSucces: 'Xóa thành công',
            ApplyText: 'Áp dụng',
            SaveText: 'Lưu',
            SaveAndAddText: 'Lưu & thêm mới',
            UseText: 'Sử dụng',
            DeleteText: 'Xóa',
            recorInpage: 'Số bản ghi/trang',
            recordText: 'bản ghi',
            StopUsingText: 'Ngưng sử dụng',
            IndividualText: 'Cá nhân',
            GroupText: 'Tập thể',
            FrequentText: 'Thường xuyên',
            BatchedText: 'Theo đợt',
            RecordText: 'Bản ghi',
            SumText: 'Tổng sổ:',
            ApplyObject: {
                Group: 'Tập thể',
                Individual: 'Cá nhân',
                GroupAndIndi: 'Tập thể và cá nhân'
            },
            MovementType: {
                Frequent: 'Thường xuyên',
                Batched: 'Theo đợt',
                FreAndBat: 'Thường xuyên, theo đợt'
            },
            EmulationCodeDuplicate: ({ named }) => `Mã danh hiệu thi đua ${named('name')} đã tồn tại trong danh sách.Xin vui lòng kiểm tra lại.`,
            messageDeleteSingle: ({ named }) => `Bạn có chắc chắn muốn xóa Danh hiệu thi đua ${named('name')} đã chọn không?`,
            messageDeleteMultiple: ({ named }) => `Xóa ${named('name')} danh hiệu đã chọn?`,
            applyObjectFilter: ({ named }) => `ApplyObject = ${named('name')}`,
            commendationLevelFilter: ({ named }) => `e.CommendationLevelID = '${named('name')}'`,
            movementTypeFilter: ({ named }) => `MovementType = ${named('name')}`,
            statusFilter: ({ named }) => `Status = ${named('name')}`
        },
    }
})

export default misaResource