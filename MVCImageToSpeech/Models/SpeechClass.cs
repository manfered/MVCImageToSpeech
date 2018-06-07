using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Web;

namespace MVCImageToSpeech.Models
{
    public class SpeechClass
    {
        private string AudioDirectory = "SpeechFiles";

        private string AudioFilesPrefic = "ImageToSpeech_";

        public SpeechClass()
        {

        }

        public void speech(OperationResult operationResult)
        {
            using (SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer())
            {
                try
                {
                    operationResult.AudioFileName = AudioFilesPrefic + Guid.NewGuid();
                    operationResult.AudioStorageDirectory = AudioDirectory;
                    string pathToStore = Path.Combine(HttpRuntime.AppDomainAppPath, operationResult.AudioStorageDirectory);
                    pathToStore += "/" + operationResult.AudioFileName + ".wav";
                    speechSynthesizer.SetOutputToWaveFile(pathToStore);
                    speechSynthesizer.Speak(operationResult.textToSpeechString);
                    operationResult.StatusStr = OperationResultStatus.Success;
                    operationResult.ResultStr += "\r\nTest successfully turned to audio";
                }
                catch (Exception ex)
                {
                    operationResult.StatusStr = OperationResultStatus.Fail;
                    operationResult.ResultStr += $"\r\nUnable to speech! - error : {ex.Message}";
                    operationResult.ResultStr += $"\r\nThe cause of this issue is permissions on the server. speech.dll needs some permissions to register keys." +
                                                    "MVCImageToSpeech.gilasak.com runs on a shared hosting which does'nt have the sufficient permissions";
                }
                
            }
        }
    }
}