"use strict";

module Affecto
{
    export class DateConverter
    {
        public static toDateTime(dateTimeWithoutTimeZone: string): Date
        {
            var dateTimeSeparator: string = "T";
            var dateSeparator: string = "-";
            var timeSeparator: string = ":";

            if (dateTimeWithoutTimeZone == null
                || dateTimeWithoutTimeZone.indexOf(dateTimeSeparator) === -1
                || dateTimeWithoutTimeZone.indexOf(dateSeparator) === -1
                || dateTimeWithoutTimeZone.indexOf(timeSeparator) === -1)
            {
                return null;
            }

            var dateAndTime: Array<string> = dateTimeWithoutTimeZone.split(dateTimeSeparator);
            var dateParts: Array<string> = dateAndTime[0].split(dateSeparator);
            var timeParts: Array<string> = dateAndTime[1].split(timeSeparator);

            if (!(NumberValidator.containsOnlyNumbers(dateParts) && NumberValidator.containsOnlyNumbers(timeParts)))
            {
                return null;
            }

            return new Date(parseInt(dateParts[0]), parseInt(dateParts[1]) - 1, parseInt(dateParts[2]), parseInt(timeParts[0]), parseInt(timeParts[1]),
                parseInt(timeParts[2]));
        }

        public static toDate(finnishDate: string): Date
        {
            try
            {
                if (!DateValidator.isFinnishDate(finnishDate))
                {
                    return null;
                }
                var dateParts: Array<string> = finnishDate.split(".");
                return new Date(Number(dateParts[2]), Number(dateParts[1]) - 1, Number(dateParts[0]));
            }
            catch (e)
            {
                return new Date(Date.parse(finnishDate));
            }
        }

        // Returns string in format yyyy-MM-dd
        public static toISO8601Date(date: any): string
        {
            return this.doConversion(date, (dateObject: Date) =>
            {
                return this.getISO8601Date(dateObject);
            });
        }

        public static toFinnishDate(date: any): string
        {
            return this.doConversion(date, (dateObject: Date) =>
            {
                return this.getFinnishDate(dateObject);
            });
        }

        public static toFinnishDateTime(dateTime: any): string
        {
            return this.doConversion(dateTime, (dateObject: Date) =>
            {
                return this.getFinnishDate(dateObject) + " " + this.getTime(dateObject);
            });
        }

        private static doConversion(input: string, conversionFunction: Function): string
        {
            if (input != null && input !== "")
            {
                var dateObject: any = new Date(input);
                if (!isNaN(dateObject.getDate()))
                {
                    return conversionFunction(dateObject);
                }
                return null;
            }
            return input;
        }

        private static getISO8601Date(date: Date): string
        {
            return date.getFullYear() + '-' + this.getMonth(date) + '-' + date.getDate();
        }

        private static getFinnishDate(date: Date): string
        {
            return date.getDate() + "." + this.getMonth(date) + "." + date.getFullYear();
        }

        private static getMonth(date: Date): number
        {
            return date.getMonth() + 1;
        }

        private static getTime(date: Date): string
        {
            return this.leftPadWithZero(date.getUTCHours()) + ":" + this.leftPadWithZero(date.getMinutes()) + ":" + this.leftPadWithZero(date.getSeconds());
        }

        private static leftPadWithZero(value: number): string
        {
            if (value < 10)
            {
                return "0" + value;
            }
            return value.toString();
        }
    }
}