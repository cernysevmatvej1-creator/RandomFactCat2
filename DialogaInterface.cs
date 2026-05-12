using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiDeepSeekl
{
    public static class DialogHelper
    {
        public static async Task ShowAlert(string title, string message, string cancel = "OK")
        {
            if (Application.Current?.MainPage != null)
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async Task<bool> ShowConfirmation(string title, string message, string accept = "Да", string cancel = "Нет")
        {
            if (Application.Current?.MainPage != null)
                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            return false;
        }

        public static async Task<string> ShowTimeSelection(string title = "Напоминание о тренировке\nВыберите время", string message = "За сколько времени вы хотите получить уведомление?")
        {
            if (Application.Current?.MainPage == null)
                return null;

            var options = new List<string>
            {
                "1 - За час",
                "2 - За 2 часа",
                "3 - За 3 часа"
            };

            var result = await Application.Current.MainPage.DisplayActionSheet(
                title,
                "Отмена",
                null,
                options.ToArray());

            return result;
        }
    }
}