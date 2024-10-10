using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AIDetection
{
    public class SightManager : MonoBehaviour
    {
        public static SightManager Instance => _instance;
        private static SightManager _instance;

        //private Dictionary<int, Detector> _detectors = new();
        private Dictionary<int, Detectee> _detectees = new();
        public static IList<Detectee> Detectees => _instance._detectees.Values.AsReadOnlyList();

        //private int _detectorCount;
        private int _detecteeCount;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("More than one instance of Sight Manager exists");
            }
        }
        
        //
        // private int RegisterDetector(Detector detector)
        // {
        //     _detectors.Add(_detectorCount, detector);
        //     _detectorCount++;
        //     return _detectorCount - 1;
        // }

        public int RegisterDetectee(Detectee detectee)
        {
            _detectees.Add(_detecteeCount, detectee);
            _detecteeCount++;
            return _detecteeCount - 1;
        }
        //
        // private void UnregisterDetector(int detectorID)
        // {
        //     _detectors.Remove(detectorID);
        // }

        private void UnregisterDetectee(int detecteeID)
        {
            _detectees.Remove(detecteeID);
        }
    }
}